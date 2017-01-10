
(function (window, $, undefined) {
    $.tw = $.tw || {};
})(window, $);
/* $.tw.url 解析器 依赖jQuery*/
(function ($) {
    var _url = function (arg, url) {
        var _ls = url || window.location.toString();
        if (!arg) {
            return _ls;
        }
        else {
            arg = arg.toString();
        }
        if (_ls.substring(0, 2) === '//') {
            _ls = 'http:' + _ls;
        }
        else if (_ls.split('://').length === 1) {
            _ls = 'http://' + _ls;
        }
        url = _ls.split('/');
        var _l = { auth: '' }, host = url[2].split('@');

        if (host.length === 1) {
            host = host[0].split(':');
        }
        else {
            _l.auth = host[0];
            host = host[1].split(':');
        }
        _l.protocol = url[0];
        _l.hostname = host[0];
        _l.port = (host[1] || '80');
        _l.pathname = ((url.length > 3 ? '/' : '') + url.slice(3, url.length).join('/').split('?')[0].split('#')[0]);
        var _p = _l.pathname;

        if (_p.charAt(_p.length - 1) === '/') {
            _p = _p.substring(0, _p.length - 1);
        }
        var _h = _l.hostname, _hs = _h.split('.'), _ps = _p.split('/');

        if (arg.charAt(0) === '?') {
            var params = _ls, param = null;
            if (arg.charAt(0) === '?') {
                params = (params.split('?')[1] || '').split('#')[0];
            }
            params = params.split('&');
            if (!arg.charAt(1)) {
                var obj = {};
                for (var i = 0, ii = params.length; i < ii; i++) {
                    param = params[i].split('=');
                    param[0] && (obj[param[0]] = decodeURIComponent(param[1] || ''));
                }
                return obj;
            }
            arg = arg.substring(1);
            for (var i = 0, ii = params.length; i < ii; i++) {
                param = params[i].split('=');
                if (param[0] === arg) {
                    return decodeURIComponent(param[1] || '');
                }
            }
            return null;
        }

        return '';
    };
    $.extend($.tw, { url: _url });
})($);
/* 分页插件 */
(function (window, $) {
    $(function () {
        //分页初始化
        $('.page').buildPage();
    });
    //生成链接
    var getLink = function (p, text, totalPage, pageparam) {
        var href = '', pageNow = +$.tw.url('?' + pageparam) || 1;
        if (p > 0 && p <= totalPage) {
            var params = $.tw.url('?');
            params[pageparam] = p;
            var query = '?';
            var i = 0;
            for (var key in params) {
                if (i == 0) {
                    query += key + '=' + params[key];
                }
                else {
                    query += '&' + key + '=' + params[key];
                }
                i++;
            }
            href = ' href="' + window.location.pathname + query + '" ';

        }
        var css = pageNow == text ? ' class="currentpage" ' : ''
            , data = p > 0 && p <= totalPage ? ' data-p="' + p + '" ' : '';
        return '<a ' + data + css + href + '>' + text + '</a>';
    };
    function StringBuffer() {
        this._strings_ = [];
    }
    StringBuffer.prototype.append = function (n) {
        this._strings_.push(n)
    }
    StringBuffer.prototype.toString = function () {
        return this._strings_.join("")
    }
    var $buildPage = function (totalCount, pageSize) {
        var $this = $(this), pageparam = 'page', pageSize = pageSize || +$this.data('size') || 10, totalCount = totalCount || +$this.data('total'), pageNow = +$.tw.url('?' + pageparam) || 1;

        if (!totalCount) {
            $this.html('');
            return;
        }
        var html = [], totalPage = Math.ceil(totalCount / pageSize);
        if (totalPage > 1) {
            var i = new StringBuffer, u = 5, o, s, f, r;
            pagecount = totalCount % pageSize != 0 ? parseInt(totalCount / pageSize) + 1 : parseInt(totalCount / pageSize),
            pageNow < 1 && (pageNow = 1),
            pagecount < 1 && (pagecount = 1),
            pageNow > pagecount && (pageNow = pagecount),
            o = parseInt(pageNow) - 1, s = parseInt(pageNow) + 1,
            o < 1 ? (i.append(' <a href="javascript:void(0);">首页<\/a>'),
            i.append(' <a href="javascript:void(0);">上一页<\/a>')) : (i.append(getLink(1, '首页', totalCount, pageparam)),
            i.append(getLink(o, '上一页', totalCount, pageparam))),
            f = pageNow % u == 0 ? pageNow - (u - 1) : pageNow - parseInt(pageNow % u) + 1,
            f > u && i.append(getLink(f - 1, '...', totalCount, pageparam));
            for (r = f; r < f + u; r++) {
                if (r > pagecount) break;
                r == pageNow ? i.append(getLink(r, r, totalCount, pageparam)) : i.append(getLink(r, r, totalCount, pageparam))
            }
            //12 10 5
            if (pagecount >= pageNow + u) {
                i.append(getLink(f + u, '...', totalCount, pageparam))
            }
            s > pagecount ? (i.append(' <a href="javascript:void(0);">下一页<\/a>'),
            i.append(' <a href="javascript:void(0);">尾页<\/a>')) : (i.append(getLink(s, '下一页', totalCount, pageparam)),
            i.append(getLink(pagecount, '尾页', totalCount, pageparam))),
            i.append(' <a href="javascript:void(0);">共' + totalPage + '页,' + totalCount + '条<\/a>'),
            html = i.toString()
        }
        $this.html(html);
        return $this;
    }
    //分页插件 paged: $paged, 
    $.fn.extend({ buildPage: $buildPage });

    for (var i = 0; i < length; i++) {

    }

})(window, $);