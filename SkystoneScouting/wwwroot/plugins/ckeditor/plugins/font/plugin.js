/*
 Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.md or http://ckeditor.com/license
*/
(function () {
    function l(a, b, c, f, p, l, t, v) {
        var w = a.config, q = new CKEDITOR.style(t), k = p.split(";"); p = []; for (var m = {}, d = 0; d < k.length; d++) { var h = k[d]; if (h) { var h = h.split("/"), u = {}, n = k[d] = h[0]; u[c] = p[d] = h[1] || n; m[n] = new CKEDITOR.style(t, u); m[n]._.definition.name = n } else k.splice(d--, 1) } a.ui.addRichCombo(b, {
            label: f.label, title: f.panelTitle, toolbar: "styles," + v, allowedContent: q, requiredContent: q, panel: { css: [CKEDITOR.skin.getPath("editor")].concat(w.contentsCss), multiSelect: !1, attributes: { "aria-label": f.panelTitle } },
            init: function () { this.startGroup(f.panelTitle); for (var a = 0; a < k.length; a++) { var b = k[a]; this.add(b, m[b].buildPreview(), b) } }, onClick: function (b) {
                a.focus(); a.fire("saveSnapshot"); var c = this.getValue(), f = m[b]; if (c && b != c) {
                    var k = m[c], e = a.getSelection().getRanges()[0]; if (e.collapsed) {
                        var d = a.elementPath(), g = d.contains(function (a) { return k.checkElementRemovable(a) }); if (g) {
                            var h = e.checkBoundaryOfElement(g, CKEDITOR.START), l = e.checkBoundaryOfElement(g, CKEDITOR.END); if (h && l) {
                                for (h = e.createBookmark(); d = g.getFirst();)d.insertBefore(g);
                                g.remove(); e.moveToBookmark(h)
                            } else h ? e.moveToPosition(g, CKEDITOR.POSITION_BEFORE_START) : l ? e.moveToPosition(g, CKEDITOR.POSITION_AFTER_END) : (e.splitElement(g), e.moveToPosition(g, CKEDITOR.POSITION_AFTER_END), r(e, d.elements.slice(), g)); a.getSelection().selectRanges([e])
                        }
                    } else a.removeStyle(k)
                } a[c == b ? "removeStyle" : "applyStyle"](f); a.fire("saveSnapshot")
            }, onRender: function () {
                a.on("selectionChange", function (b) {
                    var c = this.getValue(); b = b.data.path.elements; for (var d = 0, f; d < b.length; d++) {
                        f = b[d]; for (var e in m) if (m[e].checkElementMatch(f,
                            !0, a)) { e != c && this.setValue(e); return }
                    } this.setValue("", l)
                }, this)
            }, refresh: function () { a.activeFilter.check(q) || this.setState(CKEDITOR.TRISTATE_DISABLED) }
        })
    } function r(a, b, c) { var f = b.pop(); if (f) { if (c) return r(a, b, f.equals(c) ? null : c); c = f.clone(); a.insertNode(c); a.moveToPosition(c, CKEDITOR.POSITION_AFTER_START); r(a, b) } } CKEDITOR.plugins.add("font", {
        requires: "richcombo", lang: "af,ar,bg,bn,bs,ca,cs,cy,da,de,de-ch,el,en,en-au,en-ca,en-gb,eo,es,et,eu,fa,fi,fo,fr,fr-ca,gl,gu,he,hi,hr,hu,id,is,it,ja,ka,km,ko,ku,lt,lv,mk,mn,ms,nb,nl,no,pl,pt,pt-br,ro,ru,si,sk,sl,sq,sr,sr-latn,sv,th,tr,tt,ug,uk,vi,zh,zh-cn",
        init: function (a) { var b = a.config; l(a, "Font", "family", a.lang.font, b.font_names, b.font_defaultLabel, b.font_style, 30); l(a, "FontSize", "size", a.lang.font.fontSize, b.fontSize_sizes, b.fontSize_defaultLabel, b.fontSize_style, 40) }
    })
})(); CKEDITOR.config.font_names = "Arial/Arial, Helvetica, sans-serif;Comic Sans MS/Comic Sans MS, cursive;Courier New/Courier New, Courier, monospace;Georgia/Georgia, serif;Lucida Sans Unicode/Lucida Sans Unicode, Lucida Grande, sans-serif;Tahoma/Tahoma, Geneva, sans-serif;Times New Roman/Times New Roman, Times, serif;Trebuchet MS/Trebuchet MS, Helvetica, sans-serif;Verdana/Verdana, Geneva, sans-serif";
CKEDITOR.config.font_defaultLabel = ""; CKEDITOR.config.font_style = { element: "span", styles: { "font-family": "#(family)" }, overrides: [{ element: "font", attributes: { face: null } }] }; CKEDITOR.config.fontSize_sizes = "8/8px;9/9px;10/10px;11/11px;12/12px;14/14px;16/16px;18/18px;20/20px;22/22px;24/24px;26/26px;28/28px;36/36px;48/48px;72/72px"; CKEDITOR.config.fontSize_defaultLabel = ""; CKEDITOR.config.fontSize_style = { element: "span", styles: { "font-size": "#(size)" }, overrides: [{ element: "font", attributes: { size: null } }] };