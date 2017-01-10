/**
 * jQuery EasyUI 1.5
布局Layout
 TC改 合并4个文件
 jquery.parser.js
 jquery.resizable.js
 jquery.panel.js
 jquery.layout.js 
 *  
 *
 */
(function ($) {
    $.easyui = {
        indexOfArray: function (a, o, id) {
            for (var i = 0, _1 = a.length; i < _1; i++) {
                if (id == undefined) {
                    if (a[i] == o) {
                        return i;
                    }
                } else {
                    if (a[i][o] == id) {
                        return i;
                    }
                }
            }
            return -1;
        }, removeArrayItem: function (a, o, id) {
            if (typeof o == "string") {
                for (var i = 0, _2 = a.length; i < _2; i++) {
                    if (a[i][o] == id) {
                        a.splice(i, 1);
                        return;
                    }
                }
            } else {
                var _3 = this.indexOfArray(a, o);
                if (_3 != -1) {
                    a.splice(_3, 1);
                }
            }
        }, addArrayItem: function (a, o, r) {
            var _4 = this.indexOfArray(a, o, r ? r[o] : undefined);
            if (_4 == -1) {
                a.push(r ? r : o);
            } else {
                a[_4] = r ? r : o;
            }
        }, getArrayItem: function (a, o, id) {
            var _5 = this.indexOfArray(a, o, id);
            return _5 == -1 ? null : a[_5];
        }, forEach: function (_6, _7, _8) {
            var _9 = [];
            for (var i = 0; i < _6.length; i++) {
                _9.push(_6[i]);
            }
            while (_9.length) {
                var _a = _9.shift();
                if (_8(_a) == false) {
                    return;
                }
                if (_7 && _a.children) {
                    for (var i = _a.children.length - 1; i >= 0; i--) {
                        _9.unshift(_a.children[i]);
                    }
                }
            }
        }
    };
    $.parser = {
        auto: true, onComplete: function (_b) {
        }, plugins: ["draggable", "droppable", "resizable", "pagination", "tooltip", "linkbutton", "menu", "menubutton", "splitbutton", "switchbutton", "progressbar", "tree", "textbox", "passwordbox", "filebox", "combo", "combobox", "combotree", "combogrid", "combotreegrid", "numberbox", "validatebox", "searchbox", "spinner", "numberspinner", "timespinner", "datetimespinner", "calendar", "datebox", "datetimebox", "slider", "layout", "panel", "datagrid", "propertygrid", "treegrid", "datalist", "tabs", "accordion", "window", "dialog", "form"], parse: function (_c) {
            var aa = [];
            for (var i = 0; i < $.parser.plugins.length; i++) {
                var _d = $.parser.plugins[i];
                var r = $(".easyui-" + _d, _c);
                if (r.length) {
                    if (r[_d]) {
                        r.each(function () {
                            $(this)[_d]($.data(this, "options") || {});
                        });
                    } else {
                        aa.push({ name: _d, jq: r });
                    }
                }
            }
            if (aa.length && window.easyloader) {
                var _e = [];
                for (var i = 0; i < aa.length; i++) {
                    _e.push(aa[i].name);
                }
                easyloader.load(_e, function () {
                    for (var i = 0; i < aa.length; i++) {
                        var _f = aa[i].name;
                        var jq = aa[i].jq;
                        jq.each(function () {
                            $(this)[_f]($.data(this, "options") || {});
                        });
                    }
                    $.parser.onComplete.call($.parser, _c);
                });
            } else {
                $.parser.onComplete.call($.parser, _c);
            }
        }, parseValue: function (_10, _11, _12, _13) {
            _13 = _13 || 0;
            var v = $.trim(String(_11 || ""));
            var _14 = v.substr(v.length - 1, 1);
            if (_14 == "%") {
                v = parseInt(v.substr(0, v.length - 1));
                if (_10.toLowerCase().indexOf("width") >= 0) {
                    v = Math.floor((_12.width() - _13) * v / 100);
                } else {
                    v = Math.floor((_12.height() - _13) * v / 100);
                }
            } else {
                v = parseInt(v) || undefined;
            }
            return v;
        }, parseOptions: function (_15, _16) {
            var t = $(_15);
            var _17 = {};
            var s = $.trim(t.attr("data-options"));
            if (s) {
                if (s.substring(0, 1) != "{") {
                    s = "{" + s + "}";
                }
                _17 = (new Function("return " + s))();
            }
            $.map(["width", "height", "left", "top", "minWidth", "maxWidth", "minHeight", "maxHeight"], function (p) {
                var pv = $.trim(_15.style[p] || "");
                if (pv) {
                    if (pv.indexOf("%") == -1) {
                        pv = parseInt(pv);
                        if (isNaN(pv)) {
                            pv = undefined;
                        }
                    }
                    _17[p] = pv;
                }
            });
            if (_16) {
                var _18 = {};
                for (var i = 0; i < _16.length; i++) {
                    var pp = _16[i];
                    if (typeof pp == "string") {
                        _18[pp] = t.attr(pp);
                    } else {
                        for (var _19 in pp) {
                            var _1a = pp[_19];
                            if (_1a == "boolean") {
                                _18[_19] = t.attr(_19) ? (t.attr(_19) == "true") : undefined;
                            } else {
                                if (_1a == "number") {
                                    _18[_19] = t.attr(_19) == "0" ? 0 : parseFloat(t.attr(_19)) || undefined;
                                }
                            }
                        }
                    }
                }
                $.extend(_17, _18);
            }
            return _17;
        }
    };
    $(function () {
        var d = $("<div style=\"position:absolute;top:-1000px;width:100px;height:100px;padding:5px\"></div>").appendTo("body");
        $._boxModel = d.outerWidth() != 100;
        d.remove();
        d = $("<div style=\"position:fixed\"></div>").appendTo("body");
        $._positionFixed = (d.css("position") == "fixed");
        d.remove();
        if (!window.easyloader && $.parser.auto) {
            $.parser.parse();
        }
    });
    $.fn._outerWidth = function (_1b) {
        if (_1b == undefined) {
            if (this[0] == window) {
                return this.width() || document.body.clientWidth;
            }
            return this.outerWidth() || 0;
        }
        return this._size("width", _1b);
    };
    $.fn._outerHeight = function (_1c) {
        if (_1c == undefined) {
            if (this[0] == window) {
                return this.height() || document.body.clientHeight;
            }
            return this.outerHeight() || 0;
        }
        return this._size("height", _1c);
    };
    $.fn._scrollLeft = function (_1d) {
        if (_1d == undefined) {
            return this.scrollLeft();
        } else {
            return this.each(function () {
                $(this).scrollLeft(_1d);
            });
        }
    };
    $.fn._propAttr = $.fn.prop || $.fn.attr;
    $.fn._size = function (_1e, _1f) {
        if (typeof _1e == "string") {
            if (_1e == "clear") {
                return this.each(function () {
                    $(this).css({ width: "", minWidth: "", maxWidth: "", height: "", minHeight: "", maxHeight: "" });
                });
            } else {
                if (_1e == "fit") {
                    return this.each(function () {
                        _20(this, this.tagName == "BODY" ? $("body") : $(this).parent(), true);
                    });
                } else {
                    if (_1e == "unfit") {
                        return this.each(function () {
                            _20(this, $(this).parent(), false);
                        });
                    } else {
                        if (_1f == undefined) {
                            return _21(this[0], _1e);
                        } else {
                            return this.each(function () {
                                _21(this, _1e, _1f);
                            });
                        }
                    }
                }
            }
        } else {
            return this.each(function () {
                _1f = _1f || $(this).parent();
                $.extend(_1e, _20(this, _1f, _1e.fit) || {});
                var r1 = _22(this, "width", _1f, _1e);
                var r2 = _22(this, "height", _1f, _1e);
                if (r1 || r2) {
                    $(this).addClass("easyui-fluid");
                } else {
                    $(this).removeClass("easyui-fluid");
                }
            });
        }
        function _20(_23, _24, fit) {
            if (!_24.length) {
                return false;
            }
            var t = $(_23)[0];
            var p = _24[0];
            var _25 = p.fcount || 0;
            if (fit) {
                if (!t.fitted) {
                    t.fitted = true;
                    p.fcount = _25 + 1;
                    $(p).addClass("panel-noscroll");
                    if (p.tagName == "BODY") {
                        $("html").addClass("panel-fit");
                    }
                }
                return { width: ($(p).width() || 1), height: ($(p).height() || 1) };
            } else {
                if (t.fitted) {
                    t.fitted = false;
                    p.fcount = _25 - 1;
                    if (p.fcount == 0) {
                        $(p).removeClass("panel-noscroll");
                        if (p.tagName == "BODY") {
                            $("html").removeClass("panel-fit");
                        }
                    }
                }
                return false;
            }
        };
        function _22(_26, _27, _28, _29) {
            var t = $(_26);
            var p = _27;
            var p1 = p.substr(0, 1).toUpperCase() + p.substr(1);
            var min = $.parser.parseValue("min" + p1, _29["min" + p1], _28);
            var max = $.parser.parseValue("max" + p1, _29["max" + p1], _28);
            var val = $.parser.parseValue(p, _29[p], _28);
            var _2a = (String(_29[p] || "").indexOf("%") >= 0 ? true : false);
            if (!isNaN(val)) {
                var v = Math.min(Math.max(val, min || 0), max || 99999);
                if (!_2a) {
                    _29[p] = v;
                }
                t._size("min" + p1, "");
                t._size("max" + p1, "");
                t._size(p, v);
            } else {
                t._size(p, "");
                t._size("min" + p1, min);
                t._size("max" + p1, max);
            }
            return _2a || _29.fit;
        };
        function _21(_2b, _2c, _2d) {
            var t = $(_2b);
            if (_2d == undefined) {
                _2d = parseInt(_2b.style[_2c]);
                if (isNaN(_2d)) {
                    return undefined;
                }
                if ($._boxModel) {
                    _2d += _2e();
                }
                return _2d;
            } else {
                if (_2d === "") {
                    t.css(_2c, "");
                } else {
                    if ($._boxModel) {
                        _2d -= _2e();
                        if (_2d < 0) {
                            _2d = 0;
                        }
                    }
                    t.css(_2c, _2d + "px");
                }
            }
            function _2e() {
                if (_2c.toLowerCase().indexOf("width") >= 0) {
                    return t.outerWidth() - t.width();
                } else {
                    return t.outerHeight() - t.height();
                }
            };
        };
    };
})(jQuery);
//实际可以去掉的
(function ($) {
    var _2f = null;
    var _30 = null;
    var _31 = false;
    function _32(e) {
        if (e.touches.length != 1) {
            return;
        }
        if (!_31) {
            _31 = true;
            dblClickTimer = setTimeout(function () {
                _31 = false;
            }, 500);
        } else {
            clearTimeout(dblClickTimer);
            _31 = false;
            _33(e, "dblclick");
        }
        _2f = setTimeout(function () {
            _33(e, "contextmenu", 3);
        }, 1000);
        _33(e, "mousedown");
        if ($.fn.draggable.isDragging || $.fn.resizable.isResizing) {
            e.preventDefault();
        }
    };
    function _34(e) {
        if (e.touches.length != 1) {
            return;
        }
        if (_2f) {
            clearTimeout(_2f);
        }
        _33(e, "mousemove");
        if ($.fn.draggable.isDragging || $.fn.resizable.isResizing) {
            e.preventDefault();
        }
    };
    function _35(e) {
        if (_2f) {
            clearTimeout(_2f);
        }
        _33(e, "mouseup");
        if ($.fn.draggable.isDragging || $.fn.resizable.isResizing) {
            e.preventDefault();
        }
    };
    function _33(e, _36, _37) {
        var _38 = new $.Event(_36);
        _38.pageX = e.changedTouches[0].pageX;
        _38.pageY = e.changedTouches[0].pageY;
        _38.which = _37 || 1;
        $(e.target).trigger(_38);
    };
    if (document.addEventListener) {
        document.addEventListener("touchstart", _32, true);
        document.addEventListener("touchmove", _34, true);
        document.addEventListener("touchend", _35, true);
    }
})(jQuery);

(function ($) {
    $.fn.resizable = function (_1, _2) {
        if (typeof _1 == "string") {
            return $.fn.resizable.methods[_1](this, _2);
        }
        function _3(e) {
            var _4 = e.data;
            var _5 = $.data(_4.target, "resizable").options;
            if (_4.dir.indexOf("e") != -1) {
                var _6 = _4.startWidth + e.pageX - _4.startX;
                _6 = Math.min(Math.max(_6, _5.minWidth), _5.maxWidth);
                _4.width = _6;
            }
            if (_4.dir.indexOf("s") != -1) {
                var _7 = _4.startHeight + e.pageY - _4.startY;
                _7 = Math.min(Math.max(_7, _5.minHeight), _5.maxHeight);
                _4.height = _7;
            }
            if (_4.dir.indexOf("w") != -1) {
                var _6 = _4.startWidth - e.pageX + _4.startX;
                _6 = Math.min(Math.max(_6, _5.minWidth), _5.maxWidth);
                _4.width = _6;
                _4.left = _4.startLeft + _4.startWidth - _4.width;
            }
            if (_4.dir.indexOf("n") != -1) {
                var _7 = _4.startHeight - e.pageY + _4.startY;
                _7 = Math.min(Math.max(_7, _5.minHeight), _5.maxHeight);
                _4.height = _7;
                _4.top = _4.startTop + _4.startHeight - _4.height;
            }
        };
        function _8(e) {
            var _9 = e.data;
            var t = $(_9.target);
            t.css({ left: _9.left, top: _9.top });
            if (t.outerWidth() != _9.width) {
                t._outerWidth(_9.width);
            }
            if (t.outerHeight() != _9.height) {
                t._outerHeight(_9.height);
            }
        };
        function _a(e) {
            $.fn.resizable.isResizing = true;
            $.data(e.data.target, "resizable").options.onStartResize.call(e.data.target, e);
            return false;
        };
        function _b(e) {
            _3(e);
            if ($.data(e.data.target, "resizable").options.onResize.call(e.data.target, e) != false) {
                _8(e);
            }
            return false;
        };
        function _c(e) {
            $.fn.resizable.isResizing = false;
            _3(e, true);
            _8(e);
            $.data(e.data.target, "resizable").options.onStopResize.call(e.data.target, e);
            $(document).unbind(".resizable");
            $("body").css("cursor", "");
            return false;
        };
        return this.each(function () {
            var _d = null;
            var _e = $.data(this, "resizable");
            if (_e) {
                $(this).unbind(".resizable");
                _d = $.extend(_e.options, _1 || {});
            } else {
                _d = $.extend({}, $.fn.resizable.defaults, $.fn.resizable.parseOptions(this), _1 || {});
                $.data(this, "resizable", { options: _d });
            }
            if (_d.disabled == true) {
                return;
            }
            $(this).bind("mousemove.resizable", { target: this }, function (e) {
                if ($.fn.resizable.isResizing) {
                    return;
                }
                var _f = _10(e);
                if (_f == "") {
                    $(e.data.target).css("cursor", "");
                } else {
                    $(e.data.target).css("cursor", _f + "-resize");
                }
            }).bind("mouseleave.resizable", { target: this }, function (e) {
                $(e.data.target).css("cursor", "");
            }).bind("mousedown.resizable", { target: this }, function (e) {
                var dir = _10(e);
                if (dir == "") {
                    return;
                }
                function _11(css) {
                    var val = parseInt($(e.data.target).css(css));
                    if (isNaN(val)) {
                        return 0;
                    } else {
                        return val;
                    }
                };
                var _12 = { target: e.data.target, dir: dir, startLeft: _11("left"), startTop: _11("top"), left: _11("left"), top: _11("top"), startX: e.pageX, startY: e.pageY, startWidth: $(e.data.target).outerWidth(), startHeight: $(e.data.target).outerHeight(), width: $(e.data.target).outerWidth(), height: $(e.data.target).outerHeight(), deltaWidth: $(e.data.target).outerWidth() - $(e.data.target).width(), deltaHeight: $(e.data.target).outerHeight() - $(e.data.target).height() };
                $(document).bind("mousedown.resizable", _12, _a);
                $(document).bind("mousemove.resizable", _12, _b);
                $(document).bind("mouseup.resizable", _12, _c);
                $("body").css("cursor", dir + "-resize");
            });
            function _10(e) {
                var tt = $(e.data.target);
                var dir = "";
                var _13 = tt.offset();
                var _14 = tt.outerWidth();
                var _15 = tt.outerHeight();
                var _16 = _d.edge;
                if (e.pageY > _13.top && e.pageY < _13.top + _16) {
                    dir += "n";
                } else {
                    if (e.pageY < _13.top + _15 && e.pageY > _13.top + _15 - _16) {
                        dir += "s";
                    }
                }
                if (e.pageX > _13.left && e.pageX < _13.left + _16) {
                    dir += "w";
                } else {
                    if (e.pageX < _13.left + _14 && e.pageX > _13.left + _14 - _16) {
                        dir += "e";
                    }
                }
                var _17 = _d.handles.split(",");
                for (var i = 0; i < _17.length; i++) {
                    var _18 = _17[i].replace(/(^\s*)|(\s*$)/g, "");
                    if (_18 == "all" || _18 == dir) {
                        return dir;
                    }
                }
                return "";
            };
        });
    };
    $.fn.resizable.methods = {
        options: function (jq) {
            return $.data(jq[0], "resizable").options;
        }, enable: function (jq) {
            return jq.each(function () {
                $(this).resizable({ disabled: false });
            });
        }, disable: function (jq) {
            return jq.each(function () {
                $(this).resizable({ disabled: true });
            });
        }
    };
    $.fn.resizable.parseOptions = function (_19) {
        var t = $(_19);
        return $.extend({}, $.parser.parseOptions(_19, ["handles", { minWidth: "number", minHeight: "number", maxWidth: "number", maxHeight: "number", edge: "number" }]), { disabled: (t.attr("disabled") ? true : undefined) });
    };
    $.fn.resizable.defaults = {
        disabled: false, handles: "n, e, s, w, ne, se, sw, nw, all", minWidth: 10, minHeight: 10, maxWidth: 10000, maxHeight: 10000, edge: 5, onStartResize: function (e) {
        }, onResize: function (e) {
        }, onStopResize: function (e) {
        }
    };
    $.fn.resizable.isResizing = false;
})(jQuery);

(function ($) {
    $.fn._remove = function () {
        return this.each(function () {
            $(this).remove();
            try {
                this.outerHTML = "";
            }
            catch (err) {
            }
        });
    };
    function _1(_2) {
        _2._remove();
    };
    function _3(_4, _5) {
        var _6 = $.data(_4, "panel");
        var _7 = _6.options;
        var _8 = _6.panel;
        var _9 = _8.children(".panel-header");
        var _a = _8.children(".panel-body");
        var _b = _8.children(".panel-footer");
        if (_5) {
            $.extend(_7, { width: _5.width, height: _5.height, minWidth: _5.minWidth, maxWidth: _5.maxWidth, minHeight: _5.minHeight, maxHeight: _5.maxHeight, left: _5.left, top: _5.top });
        }
        _8._size(_7);
        _9.add(_a)._outerWidth(_8.width());
        if (!isNaN(parseInt(_7.height))) {
            _a._outerHeight(_8.height() - _9._outerHeight() - _b._outerHeight());
        } else {
            _a.css("height", "");
            var _c = $.parser.parseValue("minHeight", _7.minHeight, _8.parent());
            var _d = $.parser.parseValue("maxHeight", _7.maxHeight, _8.parent());
            var _e = _9._outerHeight() + _b._outerHeight() + _8._outerHeight() - _8.height();
            _a._size("minHeight", _c ? (_c - _e) : "");
            _a._size("maxHeight", _d ? (_d - _e) : "");
        }
        _8.css({ height: "", minHeight: "", maxHeight: "", left: _7.left, top: _7.top });
        _7.onResize.apply(_4, [_7.width, _7.height]);
        $(_4).panel("doLayout");
    };
    function _f(_10, _11) {
        var _12 = $.data(_10, "panel");
        var _13 = _12.options;
        var _14 = _12.panel;
        if (_11) {
            if (_11.left != null) {
                _13.left = _11.left;
            }
            if (_11.top != null) {
                _13.top = _11.top;
            }
        }
        _14.css({ left: _13.left, top: _13.top });
        _14.find(".tooltip-f").each(function () {
            $(this).tooltip("reposition");
        });
        _13.onMove.apply(_10, [_13.left, _13.top]);
    };
    function _15(_16) {
        $(_16).addClass("panel-body")._size("clear");
        var _17 = $("<div class=\"panel\"></div>").insertBefore(_16);
        _17[0].appendChild(_16);
        _17.bind("_resize", function (e, _18) {
            if ($(this).hasClass("easyui-fluid") || _18) {
                _3(_16);
            }
            return false;
        });
        return _17;
    };
    function _19(_1a) {
        var _1b = $.data(_1a, "panel");
        var _1c = _1b.options;
        var _1d = _1b.panel;
        _1d.css(_1c.style);
        _1d.addClass(_1c.cls);
        _1e();
        _1f();
        var _20 = $(_1a).panel("header");
        var _21 = $(_1a).panel("body");
        var _22 = $(_1a).siblings(".panel-footer");
        if (_1c.border) {
            _20.removeClass("panel-header-noborder");
            _21.removeClass("panel-body-noborder");
            _22.removeClass("panel-footer-noborder");
        } else {
            _20.addClass("panel-header-noborder");
            _21.addClass("panel-body-noborder");
            _22.addClass("panel-footer-noborder");
        }
        _20.addClass(_1c.headerCls);
        _21.addClass(_1c.bodyCls);
        $(_1a).attr("id", _1c.id || "");
        if (_1c.content) {
            $(_1a).panel("clear");
            $(_1a).html(_1c.content);
            $.parser.parse($(_1a));
        }
        function _1e() {
            if (_1c.noheader || (!_1c.title && !_1c.header)) {
                _1(_1d.children(".panel-header"));
                _1d.children(".panel-body").addClass("panel-body-noheader");
            } else {
                if (_1c.header) {
                    $(_1c.header).addClass("panel-header").prependTo(_1d);
                } else {
                    var _23 = _1d.children(".panel-header");
                    if (!_23.length) {
                        _23 = $("<div class=\"panel-header\"></div>").prependTo(_1d);
                    }
                    if (!$.isArray(_1c.tools)) {
                        _23.find("div.panel-tool .panel-tool-a").appendTo(_1c.tools);
                    }
                    _23.empty();
                    var _24 = $("<div class=\"panel-title\"></div>").html(_1c.title).appendTo(_23);
                    if (_1c.iconCls) {
                        _24.addClass("panel-with-icon");
                        $("<div class=\"panel-icon\"></div>").addClass(_1c.iconCls).appendTo(_23);
                    }
                    var _25 = $("<div class=\"panel-tool\"></div>").appendTo(_23);
                    _25.bind("click", function (e) {
                        e.stopPropagation();
                    });
                    if (_1c.tools) {
                        if ($.isArray(_1c.tools)) {
                            $.map(_1c.tools, function (t) {
                                _26(_25, t.iconCls, eval(t.handler));
                            });
                        } else {
                            $(_1c.tools).children().each(function () {
                                $(this).addClass($(this).attr("iconCls")).addClass("panel-tool-a").appendTo(_25);
                            });
                        }
                    }
                    if (_1c.collapsible) {
                        _26(_25, "panel-tool-collapse", function () {
                            if (_1c.collapsed == true) {
                                _4f(_1a, true);
                            } else {
                                _3c(_1a, true);
                            }
                        });
                    }
                    if (_1c.minimizable) {
                        _26(_25, "panel-tool-min", function () {
                            _5a(_1a);
                        });
                    }
                    if (_1c.maximizable) {
                        _26(_25, "panel-tool-max", function () {
                            if (_1c.maximized == true) {
                                _5e(_1a);
                            } else {
                                _3b(_1a);
                            }
                        });
                    }
                    if (_1c.closable) {
                        _26(_25, "panel-tool-close", function () {
                            _3d(_1a);
                        });
                    }
                }
                _1d.children("div.panel-body").removeClass("panel-body-noheader");
            }
        };
        function _26(c, _27, _28) {
            var a = $("<a href=\"javascript:void(0)\"></a>").addClass(_27).appendTo(c);
            a.bind("click", _28);
        };
        function _1f() {
            if (_1c.footer) {
                $(_1c.footer).addClass("panel-footer").appendTo(_1d);
                $(_1a).addClass("panel-body-nobottom");
            } else {
                _1d.children(".panel-footer").remove();
                $(_1a).removeClass("panel-body-nobottom");
            }
        };
    };
    function _29(_2a, _2b) {
        var _2c = $.data(_2a, "panel");
        var _2d = _2c.options;
        if (_2e) {
            _2d.queryParams = _2b;
        }
        if (!_2d.href) {
            return;
        }
        if (!_2c.isLoaded || !_2d.cache) {
            var _2e = $.extend({}, _2d.queryParams);
            if (_2d.onBeforeLoad.call(_2a, _2e) == false) {
                return;
            }
            _2c.isLoaded = false;
            if (_2d.loadingMessage) {
                $(_2a).panel("clear");
                $(_2a).html($("<div class=\"panel-loading\"></div>").html(_2d.loadingMessage));
            }
            _2d.loader.call(_2a, _2e, function (_2f) {
                var _30 = _2d.extractor.call(_2a, _2f);
                $(_2a).panel("clear");
                $(_2a).html(_30);
                $.parser.parse($(_2a));
                _2d.onLoad.apply(_2a, arguments);
                _2c.isLoaded = true;
            }, function () {
                _2d.onLoadError.apply(_2a, arguments);
            });
        }
    };
    function _31(_32) {
        var t = $(_32);
        t.find(".combo-f").each(function () {
            $(this).combo("destroy");
        });
        t.find(".m-btn").each(function () {
            $(this).menubutton("destroy");
        });
        t.find(".s-btn").each(function () {
            $(this).splitbutton("destroy");
        });
        t.find(".tooltip-f").each(function () {
            $(this).tooltip("destroy");
        });
        t.children("div").each(function () {
            $(this)._size("unfit");
        });
        t.empty();
    };
    function _33(_34) {
        $(_34).panel("doLayout", true);
    };
    function _35(_36, _37) {
        var _38 = $.data(_36, "panel").options;
        var _39 = $.data(_36, "panel").panel;
        if (_37 != true) {
            if (_38.onBeforeOpen.call(_36) == false) {
                return;
            }
        }
        _39.stop(true, true);
        if ($.isFunction(_38.openAnimation)) {
            _38.openAnimation.call(_36, cb);
        } else {
            switch (_38.openAnimation) {
                case "slide":
                    _39.slideDown(_38.openDuration, cb);
                    break;
                case "fade":
                    _39.fadeIn(_38.openDuration, cb);
                    break;
                case "show":
                    _39.show(_38.openDuration, cb);
                    break;
                default:
                    _39.show();
                    cb();
            }
        }
        function cb() {
            _38.closed = false;
            _38.minimized = false;
            var _3a = _39.children(".panel-header").find("a.panel-tool-restore");
            if (_3a.length) {
                _38.maximized = true;
            }
            _38.onOpen.call(_36);
            if (_38.maximized == true) {
                _38.maximized = false;
                _3b(_36);
            }
            if (_38.collapsed == true) {
                _38.collapsed = false;
                _3c(_36);
            }
            if (!_38.collapsed) {
                _29(_36);
                _33(_36);
            }
        };
    };
    function _3d(_3e, _3f) {
        var _40 = $.data(_3e, "panel");
        var _41 = _40.options;
        var _42 = _40.panel;
        if (_3f != true) {
            if (_41.onBeforeClose.call(_3e) == false) {
                return;
            }
        }
        _42.find(".tooltip-f").each(function () {
            $(this).tooltip("hide");
        });
        _42.stop(true, true);
        _42._size("unfit");
        if ($.isFunction(_41.closeAnimation)) {
            _41.closeAnimation.call(_3e, cb);
        } else {
            switch (_41.closeAnimation) {
                case "slide":
                    _42.slideUp(_41.closeDuration, cb);
                    break;
                case "fade":
                    _42.fadeOut(_41.closeDuration, cb);
                    break;
                case "hide":
                    _42.hide(_41.closeDuration, cb);
                    break;
                default:
                    _42.hide();
                    cb();
            }
        }
        function cb() {
            _41.closed = true;
            _41.onClose.call(_3e);
        };
    };
    function _43(_44, _45) {
        var _46 = $.data(_44, "panel");
        var _47 = _46.options;
        var _48 = _46.panel;
        if (_45 != true) {
            if (_47.onBeforeDestroy.call(_44) == false) {
                return;
            }
        }
        $(_44).panel("clear").panel("clear", "footer");
        _1(_48);
        _47.onDestroy.call(_44);
    };
    function _3c(_49, _4a) {
        var _4b = $.data(_49, "panel").options;
        var _4c = $.data(_49, "panel").panel;
        var _4d = _4c.children(".panel-body");
        var _4e = _4c.children(".panel-header").find("a.panel-tool-collapse");
        if (_4b.collapsed == true) {
            return;
        }
        _4d.stop(true, true);
        if (_4b.onBeforeCollapse.call(_49) == false) {
            return;
        }
        _4e.addClass("panel-tool-expand");
        if (_4a == true) {
            _4d.slideUp("normal", function () {
                _4b.collapsed = true;
                _4b.onCollapse.call(_49);
            });
        } else {
            _4d.hide();
            _4b.collapsed = true;
            _4b.onCollapse.call(_49);
        }
    };
    function _4f(_50, _51) {
        var _52 = $.data(_50, "panel").options;
        var _53 = $.data(_50, "panel").panel;
        var _54 = _53.children(".panel-body");
        var _55 = _53.children(".panel-header").find("a.panel-tool-collapse");
        if (_52.collapsed == false) {
            return;
        }
        _54.stop(true, true);
        if (_52.onBeforeExpand.call(_50) == false) {
            return;
        }
        _55.removeClass("panel-tool-expand");
        if (_51 == true) {
            _54.slideDown("normal", function () {
                _52.collapsed = false;
                _52.onExpand.call(_50);
                _29(_50);
                _33(_50);
            });
        } else {
            _54.show();
            _52.collapsed = false;
            _52.onExpand.call(_50);
            _29(_50);
            _33(_50);
        }
    };
    function _3b(_56) {
        var _57 = $.data(_56, "panel").options;
        var _58 = $.data(_56, "panel").panel;
        var _59 = _58.children(".panel-header").find("a.panel-tool-max");
        if (_57.maximized == true) {
            return;
        }
        _59.addClass("panel-tool-restore");
        if (!$.data(_56, "panel").original) {
            $.data(_56, "panel").original = { width: _57.width, height: _57.height, left: _57.left, top: _57.top, fit: _57.fit };
        }
        _57.left = 0;
        _57.top = 0;
        _57.fit = true;
        _3(_56);
        _57.minimized = false;
        _57.maximized = true;
        _57.onMaximize.call(_56);
    };
    function _5a(_5b) {
        var _5c = $.data(_5b, "panel").options;
        var _5d = $.data(_5b, "panel").panel;
        _5d._size("unfit");
        _5d.hide();
        _5c.minimized = true;
        _5c.maximized = false;
        _5c.onMinimize.call(_5b);
    };
    function _5e(_5f) {
        var _60 = $.data(_5f, "panel").options;
        var _61 = $.data(_5f, "panel").panel;
        var _62 = _61.children(".panel-header").find("a.panel-tool-max");
        if (_60.maximized == false) {
            return;
        }
        _61.show();
        _62.removeClass("panel-tool-restore");
        $.extend(_60, $.data(_5f, "panel").original);
        _3(_5f);
        _60.minimized = false;
        _60.maximized = false;
        $.data(_5f, "panel").original = null;
        _60.onRestore.call(_5f);
    };
    function _63(_64, _65) {
        $.data(_64, "panel").options.title = _65;
        $(_64).panel("header").find("div.panel-title").html(_65);
    };
    var _66 = null;
    $(window).unbind(".panel").bind("resize.panel", function () {
        if (_66) {
            clearTimeout(_66);
        }
        _66 = setTimeout(function () {
            var _67 = $("body.layout");
            if (_67.length) {
                _67.layout("resize");
                $("body").children(".easyui-fluid:visible").each(function () {
                    $(this).triggerHandler("_resize");
                });
            } else {
                $("body").panel("doLayout");
            }
            _66 = null;
        }, 100);
    });
    $.fn.panel = function (_68, _69) {
        if (typeof _68 == "string") {
            return $.fn.panel.methods[_68](this, _69);
        }
        _68 = _68 || {};
        return this.each(function () {
            var _6a = $.data(this, "panel");
            var _6b;
            if (_6a) {
                _6b = $.extend(_6a.options, _68);
                _6a.isLoaded = false;
            } else {
                _6b = $.extend({}, $.fn.panel.defaults, $.fn.panel.parseOptions(this), _68);
                $(this).attr("title", "");
                _6a = $.data(this, "panel", { options: _6b, panel: _15(this), isLoaded: false });
            }
            _19(this);
            $(this).show();
            if (_6b.doSize == true) {
                _6a.panel.css("display", "block");
                _3(this);
            }
            if (_6b.closed == true || _6b.minimized == true) {
                _6a.panel.hide();
            } else {
                _35(this);
            }
        });
    };
    $.fn.panel.methods = {
        options: function (jq) {
            return $.data(jq[0], "panel").options;
        }, panel: function (jq) {
            return $.data(jq[0], "panel").panel;
        }, header: function (jq) {
            return $.data(jq[0], "panel").panel.children(".panel-header");
        }, footer: function (jq) {
            return jq.panel("panel").children(".panel-footer");
        }, body: function (jq) {
            return $.data(jq[0], "panel").panel.children(".panel-body");
        }, setTitle: function (jq, _6c) {
            return jq.each(function () {
                _63(this, _6c);
            });
        }, open: function (jq, _6d) {
            return jq.each(function () {
                _35(this, _6d);
            });
        }, close: function (jq, _6e) {
            return jq.each(function () {
                _3d(this, _6e);
            });
        }, destroy: function (jq, _6f) {
            return jq.each(function () {
                _43(this, _6f);
            });
        }, clear: function (jq, _70) {
            return jq.each(function () {
                _31(_70 == "footer" ? $(this).panel("footer") : this);
            });
        }, refresh: function (jq, _71) {
            return jq.each(function () {
                var _72 = $.data(this, "panel");
                _72.isLoaded = false;
                if (_71) {
                    if (typeof _71 == "string") {
                        _72.options.href = _71;
                    } else {
                        _72.options.queryParams = _71;
                    }
                }
                _29(this);
            });
        }, resize: function (jq, _73) {
            return jq.each(function () {
                _3(this, _73);
            });
        }, doLayout: function (jq, all) {
            return jq.each(function () {
                _74(this, "body");
                _74($(this).siblings(".panel-footer")[0], "footer");
                function _74(_75, _76) {
                    if (!_75) {
                        return;
                    }
                    var _77 = _75 == $("body")[0];
                    var s = $(_75).find("div.panel:visible,div.accordion:visible,div.tabs-container:visible,div.layout:visible,.easyui-fluid:visible").filter(function (_78, el) {
                        var p = $(el).parents(".panel-" + _76 + ":first");
                        return _77 ? p.length == 0 : p[0] == _75;
                    });
                    s.each(function () {
                        $(this).triggerHandler("_resize", [all || false]);
                    });
                };
            });
        }, move: function (jq, _79) {
            return jq.each(function () {
                _f(this, _79);
            });
        }, maximize: function (jq) {
            return jq.each(function () {
                _3b(this);
            });
        }, minimize: function (jq) {
            return jq.each(function () {
                _5a(this);
            });
        }, restore: function (jq) {
            return jq.each(function () {
                _5e(this);
            });
        }, collapse: function (jq, _7a) {
            return jq.each(function () {
                _3c(this, _7a);
            });
        }, expand: function (jq, _7b) {
            return jq.each(function () {
                _4f(this, _7b);
            });
        }
    };
    $.fn.panel.parseOptions = function (_7c) {
        var t = $(_7c);
        var hh = t.children(".panel-header,header");
        var ff = t.children(".panel-footer,footer");
        return $.extend({}, $.parser.parseOptions(_7c, ["id", "width", "height", "left", "top", "title", "iconCls", "cls", "headerCls", "bodyCls", "tools", "href", "method", "header", "footer", { cache: "boolean", fit: "boolean", border: "boolean", noheader: "boolean" }, { collapsible: "boolean", minimizable: "boolean", maximizable: "boolean" }, { closable: "boolean", collapsed: "boolean", minimized: "boolean", maximized: "boolean", closed: "boolean" }, "openAnimation", "closeAnimation", { openDuration: "number", closeDuration: "number" }, ]), { loadingMessage: (t.attr("loadingMessage") != undefined ? t.attr("loadingMessage") : undefined), header: (hh.length ? hh.removeClass("panel-header") : undefined), footer: (ff.length ? ff.removeClass("panel-footer") : undefined) });
    };
    $.fn.panel.defaults = {
        id: null, title: null, iconCls: null, width: "auto", height: "auto", left: null, top: null, cls: null, headerCls: null, bodyCls: null, style: {}, href: null, cache: true, fit: false, border: true, doSize: true, noheader: false, content: null, collapsible: false, minimizable: false, maximizable: false, closable: false, collapsed: false, minimized: false, maximized: false, closed: false, openAnimation: false, openDuration: 400, closeAnimation: false, closeDuration: 400, tools: null, footer: null, header: null, queryParams: {}, method: "get", href: null, loadingMessage: "Loading...", loader: function (_7d, _7e, _7f) {
            var _80 = $(this).panel("options");
            if (!_80.href) {
                return false;
            }
            $.ajax({
                type: _80.method, url: _80.href, cache: false, data: _7d, dataType: "html", success: function (_81) {
                    _7e(_81);
                }, error: function () {
                    _7f.apply(this, arguments);
                }
            });
        }, extractor: function (_82) {
            var _83 = /<body[^>]*>((.|[\n\r])*)<\/body>/im;
            var _84 = _83.exec(_82);
            if (_84) {
                return _84[1];
            } else {
                return _82;
            }
        }, onBeforeLoad: function (_85) {
        }, onLoad: function () {
        }, onLoadError: function () {
        }, onBeforeOpen: function () {
        }, onOpen: function () {
        }, onBeforeClose: function () {
        }, onClose: function () {
        }, onBeforeDestroy: function () {
        }, onDestroy: function () {
        }, onResize: function (_86, _87) {
        }, onMove: function (_88, top) {
        }, onMaximize: function () {
        }, onRestore: function () {
        }, onMinimize: function () {
        }, onBeforeCollapse: function () {
        }, onBeforeExpand: function () {
        }, onCollapse: function () {
        }, onExpand: function () {
        }
    };
})(jQuery);

(function ($) {
    var _1 = false;
    function _2(_3, _4) {
        var _5 = $.data(_3, "layout");
        var _6 = _5.options;
        var _7 = _5.panels;
        var cc = $(_3);
        if (_4) {
            $.extend(_6, { width: _4.width, height: _4.height });
        }
        if (_3.tagName.toLowerCase() == "body") {
            cc._size("fit");
        } else {
            cc._size(_6);
        }
        var _8 = { top: 0, left: 0, width: cc.width(), height: cc.height() };
        _9(_a(_7.expandNorth) ? _7.expandNorth : _7.north, "n");
        _9(_a(_7.expandSouth) ? _7.expandSouth : _7.south, "s");
        _b(_a(_7.expandEast) ? _7.expandEast : _7.east, "e");
        _b(_a(_7.expandWest) ? _7.expandWest : _7.west, "w");
        _7.center.panel("resize", _8);
        function _9(pp, _c) {
            if (!pp.length || !_a(pp)) {
                return;
            }
            var _d = pp.panel("options");
            pp.panel("resize", { width: cc.width(), height: _d.height });
            var _e = pp.panel("panel").outerHeight();
            pp.panel("move", { left: 0, top: (_c == "n" ? 0 : cc.height() - _e) });
            _8.height -= _e;
            if (_c == "n") {
                _8.top += _e;
                if (!_d.split && _d.border) {
                    _8.top--;
                }
            }
            if (!_d.split && _d.border) {
                _8.height++;
            }
        };
        function _b(pp, _f) {
            if (!pp.length || !_a(pp)) {
                return;
            }
            var _10 = pp.panel("options");
            pp.panel("resize", { width: _10.width, height: _8.height });
            var _11 = pp.panel("panel").outerWidth();
            pp.panel("move", { left: (_f == "e" ? cc.width() - _11 : 0), top: _8.top });
            _8.width -= _11;
            if (_f == "w") {
                _8.left += _11;
                if (!_10.split && _10.border) {
                    _8.left--;
                }
            }
            if (!_10.split && _10.border) {
                _8.width++;
            }
        };
    };
    function _12(_13) {
        var cc = $(_13);
        cc.addClass("layout");
        function _14(el) {
            var _15 = $.fn.layout.parsePanelOptions(el);
            if ("north,south,east,west,center".indexOf(_15.region) >= 0) {
                _19(_13, _15, el);
            }
        };
        var _16 = cc.layout("options");
        var _17 = _16.onAdd;
        _16.onAdd = function () {
        };
        cc.find(">div,>form>div").each(function () {
            _14(this);
        });
        _16.onAdd = _17;
        cc.append("<div class=\"layout-split-proxy-h\"></div><div class=\"layout-split-proxy-v\"></div>");
        cc.bind("_resize", function (e, _18) {
            if ($(this).hasClass("easyui-fluid") || _18) {
                _2(_13);
            }
            return false;
        });
    };
    function _19(_1a, _1b, el) {
        _1b.region = _1b.region || "center";
        var _1c = $.data(_1a, "layout").panels;
        var cc = $(_1a);
        var dir = _1b.region;
        if (_1c[dir].length) {
            return;
        }
        var pp = $(el);
        if (!pp.length) {
            pp = $("<div></div>").appendTo(cc);
        }
        var _1d = $.extend({}, $.fn.layout.paneldefaults, {
            width: (pp.length ? parseInt(pp[0].style.width) || pp.outerWidth() : "auto"), height: (pp.length ? parseInt(pp[0].style.height) || pp.outerHeight() : "auto"), doSize: false, collapsible: true, onOpen: function () {
                var _1e = $(this).panel("header").children("div.panel-tool");
                _1e.children("a.panel-tool-collapse").hide();
                var _1f = { north: "up", south: "down", east: "right", west: "left" };
                if (!_1f[dir]) {
                    return;
                }
                var _20 = "layout-button-" + _1f[dir];
                var t = _1e.children("a." + _20);
                if (!t.length) {
                    t = $("<a href=\"javascript:void(0)\"></a>").addClass(_20).appendTo(_1e);
                    t.bind("click", { dir: dir }, function (e) {
                        _2d(_1a, e.data.dir);
                        return false;
                    });
                }
                $(this).panel("options").collapsible ? t.show() : t.hide();
            }
        }, _1b, { cls: ((_1b.cls || "") + " layout-panel layout-panel-" + dir), bodyCls: ((_1b.bodyCls || "") + " layout-body") });
        pp.panel(_1d);
        _1c[dir] = pp;
        var _21 = { north: "s", south: "n", east: "w", west: "e" };
        var _22 = pp.panel("panel");
        if (pp.panel("options").split) {
            _22.addClass("layout-split-" + dir);
        }
        _22.resizable($.extend({}, {
            handles: (_21[dir] || ""), disabled: (!pp.panel("options").split), onStartResize: function (e) {
                _1 = true;
                if (dir == "north" || dir == "south") {
                    var _23 = $(">div.layout-split-proxy-v", _1a);
                } else {
                    var _23 = $(">div.layout-split-proxy-h", _1a);
                }
                var top = 0, _24 = 0, _25 = 0, _26 = 0;
                var pos = { display: "block" };
                if (dir == "north") {
                    pos.top = parseInt(_22.css("top")) + _22.outerHeight() - _23.height();
                    pos.left = parseInt(_22.css("left"));
                    pos.width = _22.outerWidth();
                    pos.height = _23.height();
                } else {
                    if (dir == "south") {
                        pos.top = parseInt(_22.css("top"));
                        pos.left = parseInt(_22.css("left"));
                        pos.width = _22.outerWidth();
                        pos.height = _23.height();
                    } else {
                        if (dir == "east") {
                            pos.top = parseInt(_22.css("top")) || 0;
                            pos.left = parseInt(_22.css("left")) || 0;
                            pos.width = _23.width();
                            pos.height = _22.outerHeight();
                        } else {
                            if (dir == "west") {
                                pos.top = parseInt(_22.css("top")) || 0;
                                pos.left = _22.outerWidth() - _23.width();
                                pos.width = _23.width();
                                pos.height = _22.outerHeight();
                            }
                        }
                    }
                }
                _23.css(pos);
                $("<div class=\"layout-mask\"></div>").css({ left: 0, top: 0, width: cc.width(), height: cc.height() }).appendTo(cc);
            }, onResize: function (e) {
                if (dir == "north" || dir == "south") {
                    var _27 = $(">div.layout-split-proxy-v", _1a);
                    _27.css("top", e.pageY - $(_1a).offset().top - _27.height() / 2);
                } else {
                    var _27 = $(">div.layout-split-proxy-h", _1a);
                    _27.css("left", e.pageX - $(_1a).offset().left - _27.width() / 2);
                }
                return false;
            }, onStopResize: function (e) {
                cc.children("div.layout-split-proxy-v,div.layout-split-proxy-h").hide();
                pp.panel("resize", e.data);
                _2(_1a);
                _1 = false;
                cc.find(">div.layout-mask").remove();
            }
        }, _1b));
        cc.layout("options").onAdd.call(_1a, dir);
    };
    function _28(_29, _2a) {
        var _2b = $.data(_29, "layout").panels;
        if (_2b[_2a].length) {
            _2b[_2a].panel("destroy");
            _2b[_2a] = $();
            var _2c = "expand" + _2a.substring(0, 1).toUpperCase() + _2a.substring(1);
            if (_2b[_2c]) {
                _2b[_2c].panel("destroy");
                _2b[_2c] = undefined;
            }
            $(_29).layout("options").onRemove.call(_29, _2a);
        }
    };
    function _2d(_2e, _2f, _30) {
        if (_30 == undefined) {
            _30 = "normal";
        }
        var _31 = $.data(_2e, "layout").panels;
        var p = _31[_2f];
        var _32 = p.panel("options");
        if (_32.onBeforeCollapse.call(p) == false) {
            return;
        }
        var _33 = "expand" + _2f.substring(0, 1).toUpperCase() + _2f.substring(1);
        if (!_31[_33]) {
            _31[_33] = _34(_2f);
            var ep = _31[_33].panel("panel");
            if (!_32.expandMode) {
                ep.css("cursor", "default");
            } else {
                ep.bind("click", function () {
                    if (_32.expandMode == "dock") {
                        _41(_2e, _2f);
                    } else {
                        p.panel("expand", false).panel("open");
                        var _35 = _36();
                        p.panel("resize", _35.collapse);
                        p.panel("panel").animate(_35.expand, function () {
                            $(this).unbind(".layout").bind("mouseleave.layout", { region: _2f }, function (e) {
                                if (_1 == true) {
                                    return;
                                }
                                if ($("body>div.combo-p>div.combo-panel:visible").length) {
                                    return;
                                }
                                _2d(_2e, e.data.region);
                            });
                            $(_2e).layout("options").onExpand.call(_2e, _2f);
                        });
                    }
                    return false;
                });
            }
        }
        var _37 = _36();
        if (!_a(_31[_33])) {
            _31.center.panel("resize", _37.resizeC);
        }
        p.panel("panel").animate(_37.collapse, _30, function () {
            p.panel("collapse", false).panel("close");
            _31[_33].panel("open").panel("resize", _37.expandP);
            $(this).unbind(".layout");
            $(_2e).layout("options").onCollapse.call(_2e, _2f);
        });
        function _34(dir) {
            var _38 = { "east": "left", "west": "right", "north": "down", "south": "up" };
            var _39 = (_32.region == "north" || _32.region == "south");
            var _3a = "layout-button-" + _38[dir];
            var p = $("<div></div>").appendTo(_2e);
            p.panel($.extend({}, $.fn.layout.paneldefaults, {
                cls: ("layout-expand layout-expand-" + dir), title: "&nbsp;", iconCls: (_32.hideCollapsedContent ? null : _32.iconCls), closed: true, minWidth: 0, minHeight: 0, doSize: false, region: _32.region, collapsedSize: _32.collapsedSize, noheader: (!_39 && _32.hideExpandTool), tools: ((_39 && _32.hideExpandTool) ? null : [{
                    iconCls: _3a, handler: function () {
                        _41(_2e, _2f);
                        return false;
                    }
                }])
            }));
            if (!_32.hideCollapsedContent) {
                var _3b = typeof _32.collapsedContent == "function" ? _32.collapsedContent.call(p[0], _32.title) : _32.collapsedContent;
                _39 ? p.panel("setTitle", _3b) : p.html(_3b);
            }
            p.panel("panel").hover(function () {
                $(this).addClass("layout-expand-over");
            }, function () {
                $(this).removeClass("layout-expand-over");
            });
            return p;
        };
        function _36() {
            var cc = $(_2e);
            var _3c = _31.center.panel("options");
            var _3d = _32.collapsedSize;
            if (_2f == "east") {
                var _3e = p.panel("panel")._outerWidth();
                var _3f = _3c.width + _3e - _3d;
                if (_32.split || !_32.border) {
                    _3f++;
                }
                return { resizeC: { width: _3f }, expand: { left: cc.width() - _3e }, expandP: { top: _3c.top, left: cc.width() - _3d, width: _3d, height: _3c.height }, collapse: { left: cc.width(), top: _3c.top, height: _3c.height } };
            } else {
                if (_2f == "west") {
                    var _3e = p.panel("panel")._outerWidth();
                    var _3f = _3c.width + _3e - _3d;
                    if (_32.split || !_32.border) {
                        _3f++;
                    }
                    return { resizeC: { width: _3f, left: _3d - 1 }, expand: { left: 0 }, expandP: { left: 0, top: _3c.top, width: _3d, height: _3c.height }, collapse: { left: -_3e, top: _3c.top, height: _3c.height } };
                } else {
                    if (_2f == "north") {
                        var _40 = p.panel("panel")._outerHeight();
                        var hh = _3c.height;
                        if (!_a(_31.expandNorth)) {
                            hh += _40 - _3d + ((_32.split || !_32.border) ? 1 : 0);
                        }
                        _31.east.add(_31.west).add(_31.expandEast).add(_31.expandWest).panel("resize", { top: _3d - 1, height: hh });
                        return { resizeC: { top: _3d - 1, height: hh }, expand: { top: 0 }, expandP: { top: 0, left: 0, width: cc.width(), height: _3d }, collapse: { top: -_40, width: cc.width() } };
                    } else {
                        if (_2f == "south") {
                            var _40 = p.panel("panel")._outerHeight();
                            var hh = _3c.height;
                            if (!_a(_31.expandSouth)) {
                                hh += _40 - _3d + ((_32.split || !_32.border) ? 1 : 0);
                            }
                            _31.east.add(_31.west).add(_31.expandEast).add(_31.expandWest).panel("resize", { height: hh });
                            return { resizeC: { height: hh }, expand: { top: cc.height() - _40 }, expandP: { top: cc.height() - _3d, left: 0, width: cc.width(), height: _3d }, collapse: { top: cc.height(), width: cc.width() } };
                        }
                    }
                }
            }
        };
    };
    function _41(_42, _43) {
        var _44 = $.data(_42, "layout").panels;
        var p = _44[_43];
        var _45 = p.panel("options");
        if (_45.onBeforeExpand.call(p) == false) {
            return;
        }
        var _46 = "expand" + _43.substring(0, 1).toUpperCase() + _43.substring(1);
        if (_44[_46]) {
            _44[_46].panel("close");
            p.panel("panel").stop(true, true);
            p.panel("expand", false).panel("open");
            var _47 = _48();
            p.panel("resize", _47.collapse);
            p.panel("panel").animate(_47.expand, function () {
                _2(_42);
                $(_42).layout("options").onExpand.call(_42, _43);
            });
        }
        function _48() {
            var cc = $(_42);
            var _49 = _44.center.panel("options");
            if (_43 == "east" && _44.expandEast) {
                return { collapse: { left: cc.width(), top: _49.top, height: _49.height }, expand: { left: cc.width() - p.panel("panel")._outerWidth() } };
            } else {
                if (_43 == "west" && _44.expandWest) {
                    return { collapse: { left: -p.panel("panel")._outerWidth(), top: _49.top, height: _49.height }, expand: { left: 0 } };
                } else {
                    if (_43 == "north" && _44.expandNorth) {
                        return { collapse: { top: -p.panel("panel")._outerHeight(), width: cc.width() }, expand: { top: 0 } };
                    } else {
                        if (_43 == "south" && _44.expandSouth) {
                            return { collapse: { top: cc.height(), width: cc.width() }, expand: { top: cc.height() - p.panel("panel")._outerHeight() } };
                        }
                    }
                }
            }
        };
    };
    function _a(pp) {
        if (!pp) {
            return false;
        }
        if (pp.length) {
            return pp.panel("panel").is(":visible");
        } else {
            return false;
        }
    };
    function _4a(_4b) {
        var _4c = $.data(_4b, "layout");
        var _4d = _4c.options;
        var _4e = _4c.panels;
        var _4f = _4d.onCollapse;
        _4d.onCollapse = function () {
        };
        _50("east");
        _50("west");
        _50("north");
        _50("south");
        _4d.onCollapse = _4f;
        function _50(_51) {
            var p = _4e[_51];
            if (p.length && p.panel("options").collapsed) {
                _2d(_4b, _51, 0);
            }
        };
    };
    function _52(_53, _54, _55) {
        var p = $(_53).layout("panel", _54);
        p.panel("options").split = _55;
        var cls = "layout-split-" + _54;
        var _56 = p.panel("panel").removeClass(cls);
        if (_55) {
            _56.addClass(cls);
        }
        _56.resizable({ disabled: (!_55) });
        _2(_53);
    };
    $.fn.layout = function (_57, _58) {
        if (typeof _57 == "string") {
            return $.fn.layout.methods[_57](this, _58);
        }
        _57 = _57 || {};
        return this.each(function () {
            var _59 = $.data(this, "layout");
            if (_59) {
                $.extend(_59.options, _57);
            } else {
                var _5a = $.extend({}, $.fn.layout.defaults, $.fn.layout.parseOptions(this), _57);
                $.data(this, "layout", { options: _5a, panels: { center: $(), north: $(), south: $(), east: $(), west: $() } });
                _12(this);
            }
            _2(this);
            _4a(this);
        });
    };
    $.fn.layout.methods = {
        options: function (jq) {
            return $.data(jq[0], "layout").options;
        }, resize: function (jq, _5b) {
            return jq.each(function () {
                _2(this, _5b);
            });
        }, panel: function (jq, _5c) {
            return $.data(jq[0], "layout").panels[_5c];
        }, collapse: function (jq, _5d) {
            return jq.each(function () {
                _2d(this, _5d);
            });
        }, expand: function (jq, _5e) {
            return jq.each(function () {
                _41(this, _5e);
            });
        }, add: function (jq, _5f) {
            return jq.each(function () {
                _19(this, _5f);
                _2(this);
                if ($(this).layout("panel", _5f.region).panel("options").collapsed) {
                    _2d(this, _5f.region, 0);
                }
            });
        }, remove: function (jq, _60) {
            return jq.each(function () {
                _28(this, _60);
                _2(this);
            });
        }, split: function (jq, _61) {
            return jq.each(function () {
                _52(this, _61, true);
            });
        }, unsplit: function (jq, _62) {
            return jq.each(function () {
                _52(this, _62, false);
            });
        }
    };
    $.fn.layout.parseOptions = function (_63) {
        return $.extend({}, $.parser.parseOptions(_63, [{ fit: "boolean" }]));
    };
    $.fn.layout.defaults = {
        fit: false, onExpand: function (_64) {
        }, onCollapse: function (_65) {
        }, onAdd: function (_66) {
        }, onRemove: function (_67) {
        }
    };
    $.fn.layout.parsePanelOptions = function (_68) {
        var t = $(_68);
        return $.extend({}, $.fn.panel.parseOptions(_68), $.parser.parseOptions(_68, ["region", { split: "boolean", collpasedSize: "number", minWidth: "number", minHeight: "number", maxWidth: "number", maxHeight: "number" }]));
    };
    $.fn.layout.paneldefaults = $.extend({}, $.fn.panel.defaults, {
        region: null, split: false, collapsedSize: 28, expandMode: "float", hideExpandTool: false, hideCollapsedContent: true, collapsedContent: function (_69) {
            var p = $(this);
            var _6a = p.panel("options");
            if (_6a.region == "north" || _6a.region == "south") {
                return _69;
            }
            var _6b = _6a.collapsedSize - 2;
            var _6c = (_6b - 16) / 2;
            _6c = _6b - _6c;
            var cc = [];
            if (_6a.iconCls) {
                cc.push("<div class=\"panel-icon " + _6a.iconCls + "\"></div>");
            }
            cc.push("<div class=\"panel-title layout-expand-title");
            cc.push(_6a.iconCls ? " layout-expand-with-icon" : "");
            cc.push("\" style=\"left:" + _6c + "px\">");
            cc.push(_69);
            cc.push("</div>");
            return cc.join("");
        }, minWidth: 10, minHeight: 10, maxWidth: 10000, maxHeight: 10000
    });
})(jQuery);