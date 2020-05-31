function openModal(formTag, actionClass) {
    $.ajaxSetup({ cache: false });
    $(formTag).click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $(`${actionClass}-dialogContent`).html(data);
            $(`${actionClass}-modal`).modal('show');
        });
    });
};