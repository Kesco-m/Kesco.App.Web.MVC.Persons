(function ($) {

    $.fn.initToolTip = function(source, $container) {
        return this.each(function(index, element) {
            var domElement = this;
            initToolTip(domElement, source, $container);
        });
    };

    function initToolTip(element, source, $container) {
        var uri = '';
        if (typeof source === "string") uri = source;
        if ($.isFunction(source)) uri = source.apply(element);
        var $element = $(element);
        var $span = $("<span style='style: absolute; left: 0px; top: 0px; height: 18px;'><span style='style: relative;'></span></span>");
        if ($container)
            $container.prepend($span);
        else
            $element.parent().prepend($span);

        var pos = {
            target: $element,
            container: $span.find(':first'),
            my: $(element).data("tip-pos-my") || 'top left',
            at: $(element).data("tip-pos-at") || 'bottom left',
            viewport: $(window),
            adjust: { method: 'shift' }
        };

        if ($element.is(':input')) {
            pos.my = 'bottom left';
            pos.at = 'top left';
        }
        var options = {
            content: {
                text: '&nbsp;',
                ajax: {
                    url: uri,
                    global: true,
                    crossDomain: true,
                    beforeSend: function () {
                        $element.css('cursor', "wait !important");
                        this.set('content.text', "&nbsp;");
                        this.set('content.title.text', "");
                    },
                    success: function (data) {
                        var container = $("#dialogContentPane");
                        var width = Math.floor(container.width() / 10) * 6;
                        var height = Math.floor(container.height() / 10) * 8;
                        var content = "<div style='overflow: auto; min-width: 200px; max-width: " + width + "px; max-height:" + height + "px;'>" + data + "</div>";
                        //content = data;
                        this.set('content.text', content);
                    },
                    error: function (xhr, status, errorThrown) {
                        //do something with data
                        if (status == "parsererror") {
                            this.set('content.text', xhr.responseText);
                        }
                    },
                    complete: function () {
                        $element.css('cursor', "cursor");
                        this.set('content.title.text', "");
                    }
                },
                title: {
                    text: "&nbsp;",
                    button: 'Close'
                }
            },
            position: pos,
            events: {
                show: function (ev, api) {
                    var uri = '';
                    if (typeof source === "string") uri = source;
                    if ($.isFunction(source)) uri = source.apply(element);
                    if (!uri) {
                        try {
                            ev.preventDefault();
                        } catch (e) { }
                        return;
                    }
                    if (uri != api.get('content.ajax.url')) {
                        api.set('content.ajax.url', uri);
                    }
                },
                visible: function (ev, api) {
                    if (!$('#ui-tooltip-' + api.get('id')).data('resizable')) {
                        $('#ui-tooltip-' + api.get('id')).resizable({
                            minWidth: 320, minHeight: 160,
                            start: function () {
                                api.set({ 'hide.event': false });
                            },
                            stop: function () {
                                api.set({ 'hide.event': 'mouseleave' });
                            }
                        });
                    }
                }
            },
            show: { event: 'mouseenter', solo: true, delay: 1000 },
            hide: { delay: 500, fixed: true },
            style: { classes: 'ui-tooltip-shadow', widget: true }
        };

        $element.qtip(options);
    }

} (jQuery));