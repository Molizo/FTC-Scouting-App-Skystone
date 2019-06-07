/*
 Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.md or http://ckeditor.com/license
*/
CKEDITOR.plugins.add("notification", {
    lang: "cs,da,de,de-ch,en,eo,eu,fr,gl,id,it,km,ko,ku,nb,nl,pl,pt,pt-br,ru,sv,tr,ug,uk,zh,zh-cn", requires: "toolbar", init: function (b) {
        function a(b) { var a = new CKEDITOR.dom.element("div"); a.setStyles({ position: "fixed", "margin-left": "-9999px" }); a.setAttributes({ "aria-live": "assertive", "aria-atomic": "true" }); a.setText(b); CKEDITOR.document.getBody().append(a); setTimeout(function () { a.remove() }, 100) } b._.notificationArea = new Area(b); b.showNotification = function (a, d, e) {
            var g,
                m; "progress" == d ? g = e : m = e; a = new CKEDITOR.plugins.notification(b, { message: a, type: d, progress: g, duration: m }); a.show(); return a
        }; b.on("key", function (c) { if (27 == c.data.keyCode) { var d = b._.notificationArea.notifications; d.length && (a(b.lang.notification.closed), d[d.length - 1].hide(), c.cancel()) } })
    }
});
function Notification(b, a) { CKEDITOR.tools.extend(this, a, { editor: b, id: "cke-" + CKEDITOR.tools.getUniqueId(), area: b._.notificationArea }); a.type || (this.type = "info"); this.element = this._createElement(); b.plugins.clipboard && CKEDITOR.plugins.clipboard.preventDefaultDropOnElement(this.element) }
Notification.prototype = {
    show: function () { !1 !== this.editor.fire("notificationShow", { notification: this }) && (this.area.add(this), this._hideAfterTimeout()) }, update: function (b) {
        var a = !0; !1 === this.editor.fire("notificationUpdate", { notification: this, options: b }) && (a = !1); var c = this.element, d = c.findOne(".cke_notification_message"), e = c.findOne(".cke_notification_progress"), g = b.type; c.removeAttribute("role"); b.progress && "progress" != this.type && (g = "progress"); g && (c.removeClass(this._getClass()), c.removeAttribute("aria-label"),
            this.type = g, c.addClass(this._getClass()), c.setAttribute("aria-label", this.type), "progress" != this.type || e ? "progress" != this.type && e && e.remove() : (e = this._createProgressElement(), e.insertBefore(d))); void 0 !== b.message && (this.message = b.message, d.setHtml(this.message)); void 0 !== b.progress && (this.progress = b.progress, e && e.setStyle("width", this._getPercentageProgress())); a && b.important && (c.setAttribute("role", "alert"), this.isVisible() || this.area.add(this)); this.duration = b.duration; this._hideAfterTimeout()
    },
    hide: function () { !1 !== this.editor.fire("notificationHide", { notification: this }) && this.area.remove(this) }, isVisible: function () { return 0 <= CKEDITOR.tools.indexOf(this.area.notifications, this) }, _createElement: function () {
        var b = this, a, c, d = this.editor.lang.common.close; a = new CKEDITOR.dom.element("div"); a.addClass("cke_notification"); a.addClass(this._getClass()); a.setAttributes({ id: this.id, role: "alert", "aria-label": this.type }); "progress" == this.type && a.append(this._createProgressElement()); c = new CKEDITOR.dom.element("p");
        c.addClass("cke_notification_message"); c.setHtml(this.message); a.append(c); c = CKEDITOR.dom.element.createFromHtml('\x3ca class\x3d"cke_notification_close" href\x3d"javascript:void(0)" title\x3d"' + d + '" role\x3d"button" tabindex\x3d"-1"\x3e\x3cspan class\x3d"cke_label"\x3eX\x3c/span\x3e\x3c/a\x3e'); a.append(c); c.on("click", function () { b.editor.focus(); b.hide() }); return a
    }, _getClass: function () { return "progress" == this.type ? "cke_notification_info" : "cke_notification_" + this.type }, _createProgressElement: function () {
        var b =
            new CKEDITOR.dom.element("span"); b.addClass("cke_notification_progress"); b.setStyle("width", this._getPercentageProgress()); return b
    }, _getPercentageProgress: function () { return Math.round(100 * (this.progress || 0)) + "%" }, _hideAfterTimeout: function () {
        var b = this, a; this._hideTimeoutId && clearTimeout(this._hideTimeoutId); if ("number" == typeof this.duration) a = this.duration; else if ("info" == this.type || "success" == this.type) a = "number" == typeof this.editor.config.notification_duration ? this.editor.config.notification_duration :
            5E3; a && (b._hideTimeoutId = setTimeout(function () { b.hide() }, a))
    }
}; function Area(b) { var a = this; this.editor = b; this.notifications = []; this.element = this._createElement(); this._uiBuffer = CKEDITOR.tools.eventsBuffer(10, this._layout, this); this._changeBuffer = CKEDITOR.tools.eventsBuffer(500, this._layout, this); b.on("destroy", function () { a._removeListeners(); a.element.remove() }) }
Area.prototype = {
    add: function (b) { this.notifications.push(b); this.element.append(b.element); 1 == this.element.getChildCount() && (CKEDITOR.document.getBody().append(this.element), this._attachListeners()); this._layout() }, remove: function (b) { var a = CKEDITOR.tools.indexOf(this.notifications, b); 0 > a || (this.notifications.splice(a, 1), b.element.remove(), this.element.getChildCount() || (this._removeListeners(), this.element.remove())) }, _createElement: function () {
        var b = this.editor, a = b.config, c = new CKEDITOR.dom.element("div");
        c.addClass("cke_notifications_area"); c.setAttribute("id", "cke_notifications_area_" + b.name); c.setStyle("z-index", a.baseFloatZIndex - 2); return c
    }, _attachListeners: function () { var b = CKEDITOR.document.getWindow(), a = this.editor; b.on("scroll", this._uiBuffer.input); b.on("resize", this._uiBuffer.input); a.on("change", this._changeBuffer.input); a.on("floatingSpaceLayout", this._layout, this, null, 20); a.on("blur", this._layout, this, null, 20) }, _removeListeners: function () {
        var b = CKEDITOR.document.getWindow(), a = this.editor;
        b.removeListener("scroll", this._uiBuffer.input); b.removeListener("resize", this._uiBuffer.input); a.removeListener("change", this._changeBuffer.input); a.removeListener("floatingSpaceLayout", this._layout); a.removeListener("blur", this._layout)
    }, _layout: function () {
        function b() { a.setStyle("left", k(n + d.width - f - h)) } var a = this.element, c = this.editor, d = c.ui.contentsElement.getClientRect(), e = c.ui.contentsElement.getDocumentPosition(), c = c.ui.space("top"), g = c.getClientRect(), m = a.getClientRect(), l, f = this._notificationWidth,
            h = this._notificationMargin; l = CKEDITOR.document.getWindow(); var p = l.getScrollPosition(), q = l.getViewPaneSize(), r = CKEDITOR.document.getBody(), t = r.getDocumentPosition(), k = CKEDITOR.tools.cssLength; f && h || (l = this.element.getChild(0), f = this._notificationWidth = l.getClientRect().width, h = this._notificationMargin = parseInt(l.getComputedStyle("margin-left"), 10) + parseInt(l.getComputedStyle("margin-right"), 10)); c.isVisible() && g.bottom > d.top && g.bottom < d.bottom - m.height ? a.setStyles({ position: "fixed", top: k(g.bottom) }) :
                0 < d.top ? a.setStyles({ position: "absolute", top: k(e.y) }) : e.y + d.height - m.height > p.y ? a.setStyles({ position: "fixed", top: 0 }) : a.setStyles({ position: "absolute", top: k(e.y + d.height - m.height) }); var n = "fixed" == a.getStyle("position") ? d.left : "static" != r.getComputedStyle("position") ? e.x - t.x : e.x; d.width < f + h ? e.x + f + h > p.x + q.width ? b() : a.setStyle("left", k(n)) : e.x + f + h > p.x + q.width ? a.setStyle("left", k(n)) : e.x + d.width / 2 + f / 2 + h > p.x + q.width ? a.setStyle("left", k(n - e.x + p.x + q.width - f - h)) : 0 > d.left + d.width - f - h ? b() : 0 > d.left + d.width /
                    2 - f / 2 ? a.setStyle("left", k(n - e.x + p.x)) : a.setStyle("left", k(n + d.width / 2 - f / 2 - h / 2))
    }
}; CKEDITOR.plugins.notification = Notification;