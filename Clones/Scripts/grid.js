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
                                $('#edit-table-body').load(grid.attrData.update);
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
                                    $('#edit-table-body').load(grid.attrData.update);
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
                                    $('#edit-table-body').load(grid.attrData.update);
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

        init: function () {
            this.deleteCheck.init();
            this.deleteConfirm.init();
            this.create.init();
            this.save.init();
        }
    };

    grid.init();

    bindingEvents(eventsData);
});