$(document).ready(function ()
{
    var GetResponseMessage = function (message) {
        var time = Date.now();
        var response_tmpl = '<span id="' + time + '" style="padding: 10px; width: 300px; background: green;">' + message + '</span>';

        $('.j-response').append(response_tmpl);
        setTimeout(function () { $('#' + time).remove(); }, 2000);
    };

    var SaveModel = function ($this) {
        var dataAttr = $('#content-table').data();
        var id = $this.data('id');
        var field = $('#model-' + id);

        if ($this.parents('form').valid()) {
            var data = {};
            var data_container = field.find('input, textarea, select');
            $.each(data_container, function (i, el) {
                var res = $(el).attr('name').replace('-' + id, '').replace('item.', '');
                data[res] = $(el).val();
            });

            $.ajax({
                url: dataAttr.save,
                method: 'Post',
                data: data,
                success: function (data, p1, p2) {
                    GetResponseMessage('Save');

                    field.html(data);
                }
            });
        }
    };

    $('.j-show').click(function (e) {
        var hiddenBlock = $(this).data('show');
        $(hiddenBlock).toggle('hide');
    });

    var DeleteModel = function ($this) {
        var id = $this.data('id');
        var name = $this.data('name');

        $('.j-delete-model-name').html(' ' + name);
        $('#confirm-delete').attr('data-id', id);
    };

    $('#confirm-delete').click(function () {
        var dataAttr = $('#content-table').data();
        var id = $(this).data('id');

        $.ajax({
            url: dataAttr.delete + '/' + id,
            method: 'Post',
            success: function (data, p1, p2) {
                GetResponseMessage('Delete');

                $('#content-table').load(dataAttr.update);
            }
        });
    });

    $('#content-table').on('click', '.j-delete-model', function (e) {
        e.preventDefault();
        DeleteModel($(this));
    });

    $('#content-table').on('click', '.j-save-model', function (e) {
        e.preventDefault();
        SaveModel($(this));
    });

    var CreateModelManagement = function () {
        var $createBtn = $('.j-create'),
            $createBtnPost = $('.j-create-model'),
            createUrl = $createBtn.data('create'),
            tableUpdateUrl = $('#content-table').data('update');

        var CreateModel = function ($this) {
            var $form = $this.parents('form');

            if ($form.valid()) {
                $.ajax({
                    url: createUrl,
                    method: 'Post',
                    data: $form.serialize(),
                    success: function (data) {
                        switch (data.ResultState) {
                            case 1:
                                $('#content-table').load(tableUpdateUrl);
                                $form.trigger('reset');
                                GetResponseMessage('Create');
                                break;
                            default:
                                console.log('error');
                                break;
                        }
                    }
                });
            }
        };

        $createBtnPost.click(function (e) {
            e.preventDefault();
            CreateModel($(this));
        });
    };

    CreateModelManagement();
});

//function AjaxFormSuccess(data, status, data1) {
//    //window.location.reload();
//    console.log(data);
//    console.log(status);
//    console.log(data1);
//}