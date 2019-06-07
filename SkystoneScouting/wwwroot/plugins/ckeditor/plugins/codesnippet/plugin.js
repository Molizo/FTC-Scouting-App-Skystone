/*
 Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.md or http://ckeditor.com/license
*/
(function () {
    function m(a) { CKEDITOR.tools.extend(this, a); this.queue = []; this.init ? this.init(CKEDITOR.tools.bind(function () { for (var a; a = this.queue.pop();)a.call(this); this.ready = !0 }, this)) : this.ready = !0 } function p(a) {
        var b = a.config.codeSnippet_codeClass, e = /\r?\n/g, h = new CKEDITOR.dom.element("textarea"); a.widgets.add("codeSnippet", {
            allowedContent: "pre; code(language-*)", requiredContent: "pre", styleableElements: "pre", template: '\x3cpre\x3e\x3ccode class\x3d"' + b + '"\x3e\x3c/code\x3e\x3c/pre\x3e', dialog: "codeSnippet",
            pathName: a.lang.codesnippet.pathName, mask: !0, parts: { pre: "pre", code: "code" }, highlight: function () { var d = this, c = this.data, b = function (a) { d.parts.code.setHtml(n ? a : a.replace(e, "\x3cbr\x3e")) }; b(CKEDITOR.tools.htmlEncode(c.code)); a._.codesnippet.highlighter.highlight(c.code, c.lang, function (d) { a.fire("lockSnapshot"); b(d); a.fire("unlockSnapshot") }) }, data: function () {
                var a = this.data, b = this.oldData; a.code && this.parts.code.setHtml(CKEDITOR.tools.htmlEncode(a.code)); b && a.lang != b.lang && this.parts.code.removeClass("language-" +
                    b.lang); a.lang && (this.parts.code.addClass("language-" + a.lang), this.highlight()); this.oldData = CKEDITOR.tools.copy(a)
            }, upcast: function (d, c) {
                if ("pre" == d.name) {
                    for (var g = [], e = d.children, k, l = e.length - 1; 0 <= l; l--)k = e[l], k.type == CKEDITOR.NODE_TEXT && k.value.match(q) || g.push(k); var f; if (1 == g.length && "code" == (f = g[0]).name && 1 == f.children.length && f.children[0].type == CKEDITOR.NODE_TEXT) {
                        if (g = a._.codesnippet.langsRegex.exec(f.attributes["class"])) c.lang = g[1]; h.setHtml(f.getHtml()); c.code = h.getValue(); f.addClass(b);
                        return d
                    }
                }
            }, downcast: function (a) { var c = a.getFirst("code"); c.children.length = 0; c.removeClass(b); c.add(new CKEDITOR.htmlParser.text(CKEDITOR.tools.htmlEncode(this.data.code))); return a }
        }); var q = /^[\s\n\r]*$/
    } var n = !CKEDITOR.env.ie || 8 < CKEDITOR.env.version; CKEDITOR.plugins.add("codesnippet", {
        requires: "widget,dialog", lang: "ar,bg,ca,cs,da,de,de-ch,el,en,en-gb,eo,es,et,eu,fa,fi,fr,fr-ca,gl,he,hr,hu,id,it,ja,km,ko,ku,lt,lv,nb,nl,no,pl,pt,pt-br,ro,ru,sk,sl,sq,sv,th,tr,tt,ug,uk,vi,zh,zh-cn", icons: "codesnippet",
        hidpi: !0, beforeInit: function (a) { a._.codesnippet = {}; this.setHighlighter = function (b) { a._.codesnippet.highlighter = b; b = a._.codesnippet.langs = a.config.codeSnippet_languages || b.languages; a._.codesnippet.langsRegex = new RegExp("(?:^|\\s)language-(" + CKEDITOR.tools.objectKeys(b).join("|") + ")(?:\\s|$)") } }, onLoad: function () { CKEDITOR.dialog.add("codeSnippet", this.path + "dialogs/codesnippet.js") }, init: function (a) {
            a.ui.addButton && a.ui.addButton("CodeSnippet", {
                label: a.lang.codesnippet.button, command: "codeSnippet",
                toolbar: "insert,10"
            })
        }, afterInit: function (a) {
            var b = this.path; p(a); if (!a._.codesnippet.highlighter) {
                var e = new CKEDITOR.plugins.codesnippet.highlighter({
                    languages: {
                        apache: "Apache", bash: "Bash", coffeescript: "CoffeeScript", cpp: "C++", cs: "C#", css: "CSS", diff: "Diff", html: "HTML", http: "HTTP", ini: "INI", java: "Java", javascript: "JavaScript", json: "JSON", makefile: "Makefile", markdown: "Markdown", nginx: "Nginx", objectivec: "Objective-C", perl: "Perl", php: "PHP", python: "Python", ruby: "Ruby", sql: "SQL", vbscript: "VBScript", xhtml: "XHTML",
                        xml: "XML"
                    }, init: function (h) { var e = this; n && CKEDITOR.scriptLoader.load(b + "lib/highlight/highlight.pack.js", function () { e.hljs = window.hljs; h() }); a.addContentsCss && a.addContentsCss(b + "lib/highlight/styles/" + a.config.codeSnippet_theme + ".css") }, highlighter: function (a, b, d) { (a = this.hljs.highlightAuto(a, this.hljs.getLanguage(b) ? [b] : void 0)) && d(a.value) }
                }); this.setHighlighter(e)
            }
        }
    }); CKEDITOR.plugins.codesnippet = { highlighter: m }; m.prototype.highlight = function () {
        var a = arguments; this.ready ? this.highlighter.apply(this,
            a) : this.queue.push(function () { this.highlighter.apply(this, a) })
    }
})(); CKEDITOR.config.codeSnippet_codeClass = "hljs"; CKEDITOR.config.codeSnippet_theme = "default";