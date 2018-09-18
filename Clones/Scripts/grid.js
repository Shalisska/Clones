;
$(document).ready(function () {
    var eventsData = [];

    var buildEventData = function (container, event, sender, func) {
        return {
            container: container,
            event: event,
            sender: sender,
            func: func
        };
    };

    var bindEvent = function ($container, event, sender, func) {
        $container.on(event, sender, function (e) {
            func($(this), e);
        });
    };

    var bindingEvents = function (eventDataArr) {
        for (var i = 0; i < eventDataArr.length; i++) {
            var eventData = eventDataArr[i];
            bindEvent(eventData.container, eventData.event, eventData.sender, eventData.func);
        }
    };

    var responseMessage = function (message) {
        var time = Date.now();
        var response_tmpl = '<span id="' + time + '" style="padding: 10px; width: 300px; background: green;">' + message + '</span>';

        $('.j-response').append(response_tmpl);
        setTimeout(function () { $('#' + time).remove(); }, 2000);
    };

    $('.j-show').click(function (e) {
        var hiddenBlock = $(this).data('show');
        $(hiddenBlock).toggle('hide');
    });

    $(document).keydown(function(event) {
        // If Control or Command key is pressed and the S key is pressed
        // run save function. 83 is the key code for S.
        if((event.ctrlKey || event.metaKey) && event.which == 83) {
            // Save Function
            event.preventDefault();
            grid.save.saveModels();
        }
    });

    var numberInputs = 'input[data-val-number]:not([type="hidden"])';

    $('body').on('change', numberInputs, function () {
        var val = $(this).val();
        var newVal = val.replace('.', ',');
        $(this).val(newVal);
    });

    var grid = {
        container: $('#content-table'),
        attrData: $('#content-table').data(),

        deleteCheck: {
            sender: '#delete-all',
            click: function ($this, e) {
                var $checkAll = $this;
                var $checkItem = $('.j-delete-checkbox');
                var check = $checkAll.prop('checked');
                $checkItem.prop('checked', check);
            },
            init: function () {
                eventsData.push(buildEventData(grid.container, 'click', this.sender, this.click));
            }
        },

        deleteConfirm: {
            sender: '#confirm-delete',
            click: function () {
                var ids = [];
                var $items = $('.j-delete-checkbox').filter(':checked');
                $items.each(function () {
                    ids.push($(this).val());
                });

                this.post(ids);
            },
            post: function (ids) {
                $.ajax({
                    url: grid.attrData.delete,
                    method: 'Post',
                    data: { ids: ids },
                    success: function (data) {
                        switch (data.ResultState) {
                            case 1:
                                grid.tableParameters.sendRequest(grid.tableParameters.getTableParameters());
                                responseMessage('Delete');
                                break;
                            default:
                                responseMessage('error');
                                break;
                        }
                    }
                });
            },
            init: function () {
                var $root = this;
                $($root.sender).click(function () { $root.click() });
            }
        },

        create: {
            sender: '.j-create-model',
            click: function ($this, e) {
                e.preventDefault();
                var $form = $this.parents('form');

                if ($form.valid()) {
                    $.ajax({
                        url: grid.attrData.create,
                        method: 'Post',
                        data: $form.serialize(),
                        success: function (data) {
                            switch (data.ResultState) {
                                case 1:
                                    grid.tableParameters.sendRequest(grid.tableParameters.getTableParameters());
                                    $form.trigger('reset');
                                    responseMessage('Create');
                                    break;
                                default:
                                    responseMessage('error');
                                    break;
                            }
                        }
                    });
                }
            },
            init: function () {
                eventsData.push(buildEventData(grid.container, 'click', this.sender, this.click));
            }
        },

        save: {
            sender: '.j-save-models',
            click: function ($this, e) {
                e.preventDefault();

                grid.save.saveModels();
            },
            saveModels: function () {
                var $form = $('#edit-table').parent('form');
                var data = [];

                var getChangedModel = function ($row) {
                    var $rowItems = $($row.find('.j-model-field'));
                    var data = {};
                    $rowItems.each(function () {
                        var fieldName = $(this).data('name');
                        data[fieldName] = $(this).val();
                    });

                    return data;
                };

                if ($form.valid()) {
                    var $changedRows = $('.j-changed-model');
                    $changedRows.each(function () {
                        data.push(getChangedModel($(this)));
                    });

                    $.ajax({
                        url: grid.attrData.save,
                        method: 'Post',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(data),
                        success: function (data) {
                            switch (data.ResultState) {
                                case 1:
                                    grid.tableParameters.sendRequest(grid.tableParameters.getTableParameters());
                                    responseMessage('Save');
                                    break;
                                default:
                                    responseMessage('error');
                                    break;
                            }
                        }
                    });
                }
            },
            changeValue: {
                sender: '#edit-table .j-model-field',
                change: function ($this) {
                    $this.parent('td').addClass('changed');
                    $this.parents('tr').addClass('j-changed-model');
                }
            },
            init: function () {
                eventsData.push(buildEventData(grid.container, 'change', this.changeValue.sender, this.changeValue.change));
                eventsData.push(buildEventData(grid.container, 'click', this.sender, this.click))
            }
        },

        filtersBlock: {
            btn: '.j-editable',
            container: '.j-filters-box',
            click: function($this, e) {
                e.preventDefault();

                var $container = $this.siblings(grid.filtersBlock.container);

                if ($container.hasClass('active'))
                    $container.removeClass('active');
                else {
                    $(grid.filtersBlock.container).removeClass('active');
                    $container.addClass('active');
                }
            },
            init: function () {
                eventsData.push(buildEventData(grid.container, 'click', this.btn, this.click));
            }
        },

        tableParameters: {
            storage: {
                sort: {},
                filters: {
                    byName: []
                }
            },

            addSortParameter: function (name, value, isRequest = true) {
                var sortParameters = grid.tableParameters.storage.sort;

                if (sortParameters[name])
                    delete sortParameters[name];

                if (value !== "none")
                    sortParameters[name] = value;

                if (isRequest)
                    grid.tableParameters.sendRequest(grid.tableParameters.getTableParameters());
            },

            addFilterParameters: function (name, values) {
                var filterParameters = grid.tableParameters.storage.filters;
                var filterParametersByName = filterParameters.byName;

                filterParametersByName = jQuery.grep(filterParametersByName, function (value) {
                    return value.Key != name;
                });

                if (values.byName.length > 0)
                    filterParametersByName.push({ Key: name, Value: values.byName });

                grid.tableParameters.storage.filters.byName = filterParametersByName;
                grid.tableParameters.sendRequest(grid.tableParameters.getTableParameters());
            },

            getTableParameters: function () {
                return grid.tableParameters.storage;
            },

            sendRequest: function (tableParameters) {
                $.ajax({
                    url: grid.attrData.update,
                    method: 'Post',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(tableParameters),
                    success: function (data) {
                        $('#edit-table-body').html(data);
                    }
                });
            }
        },

        sort: {
            sender: '.j-sort',
            click: function ($this, e) {
                e.preventDefault();

                grid.sort.changeSort($this);
            },
            changeSort: function ($this) {
                var parameterName = $this.data().name;
                var oldSortingOrder = $this.attr('data-sort');
                var newSortingOrder = grid.sort.getNextSortingOrderValue(oldSortingOrder);

                grid.tableParameters.addSortParameter(parameterName, newSortingOrder);
                grid.sort.update($this, oldSortingOrder, newSortingOrder);
            },
            update: function ($this, oldSortingOrder, newSortingOrder) {
                $this.attr('data-sort', newSortingOrder);

                $this.removeClass(oldSortingOrder);
                $this.addClass(newSortingOrder);
            },
            getNextSortingOrderValue: function (sortingOrderValue) {
                switch (sortingOrderValue) {
                    case 'none':
                        return 'up';
                    case 'up':
                        return 'down';
                    case 'down':
                        return 'none';
                    default:
                        return 'up';
                };
            },
            getSortingValues: function () {
                $(this.sender).each(function () {
                    var $this = $(this);
                    var sortValue = $this.attr('data-sort');

                    if (sortValue !== 'none')
                        grid.tableParameters.addSortParameter($this.data().name, sortValue, false);
                });
            },
            init: function () {
                eventsData.push(buildEventData(grid.container, 'click', this.sender, this.click));
                grid.sort.getSortingValues();
            }
        },
        filters: {
            container: '.j-filters',
            btn: '.j-filters-apply',
            btnCheckAll: '.j-filters-checkbox-all',
            click: function ($this, e) {
                e.preventDefault();

                grid.filters.changeFilter($this);
            },
            changeFilter: function ($this) {
                var container = $this.parent(this.container);
                var columnName = $(container).data().name;

                var values = {
                    byName: this.getFilterValuesByName($this, container)
                };

                grid.tableParameters.addFilterParameters(columnName, values);
            },

            getFilterValuesByName: function ($this, container) {
                var checkboxes = container.find('.j-filters-checkbox').filter(':checked');
                var values = [];

                $(checkboxes).each(function () {
                    values.push($(this).data().filter);
                });
                return values;
            },
            getFilterValuesByString: function ($this, container) { },
            getFilterValuesByRange: function ($this, container) { },


            clickCheckAll: function ($this, e) {
                var $checkItem = $this.parents(grid.filters.container).find('.j-filters-checkbox');
                var check = $this.prop('checked');
                $checkItem.prop('checked', check);
            },

            init: function () {
                eventsData.push(buildEventData(grid.container, 'click', this.btn, this.click));
                eventsData.push(buildEventData(grid.container, 'click', this.btnCheckAll, this.clickCheckAll));
            }
        },

        init: function () {
            this.deleteCheck.init();
            this.deleteConfirm.init();
            this.create.init();
            this.save.init();
            this.sort.init();
            this.filters.init();
            this.filtersBlock.init();
        }
    };

    grid.init();

    bindingEvents(eventsData);
});