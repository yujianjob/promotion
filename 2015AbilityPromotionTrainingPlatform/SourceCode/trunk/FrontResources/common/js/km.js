
(function() {
  var method;
  var noop = function() {};
  var methods = [
      'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
      'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
      'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
      'timeStamp', 'trace', 'warn'
  ];
  var length = methods.length;
  var console = (window.console = window.console || {});

  while (length--) {
    method = methods[length];

    // Only stub undefined methods.
    if (!console[method]) {
      console[method] = noop;
    }
  }
}());
var km_jc = {
  loadresource:function(fn, ha) {
    km_jc.fn = (typeof fn == 'undefined') ? function() {} : fn;
    if (ha) {$proto.himgs = ha;}
    if ($proto.himgs.length == 0) {km_jc.fn();return;}
    var imgO = new km_image();
    imgO.himgs = {};
    imgO.func = function() {};
    imgO.after = function() {
      for (var i in $proto.himgs) {
        $proto.himgs[i].size = imgO.size[i];
      }
      setTimeout(function() {km_jc.fn();}, 1);
    };
    for (var i in $proto.himgs) {
      imgO.himg[i]=$proto.himgs[i].bg;
    }
    imgO.init();
  }  , loadblock:function(fn) {
    km_jc.fn = (typeof fn == 'undefined') ? function() {} : fn;
    km_cmdlist['block'] = new km_cl('block');
    var size = $('[data-km-htm]').size();
    if (size == 0) {
      km_cmdlist['block'] = null;
      try {delete km_cmdlist['block'];}catch (e) {}
      km_jc.fn();
      return;
    }
    for (var i = 0; i < size; i++) {
      (function() {
        var _eo = $('[data-km-htm]').eq(i);
        km_cmdlist['block'].cmdA.push({func:function() {
          km_jc.loadblockdata(_eo);
        }});
      })();
    }
    km_cmdlist['block'].cmdA.push({func:function() {
      km_cmdlist['block'] = null;
      try {delete km_cmdlist['block'];}catch (e) {}
      setTimeout(function() {km_jc.fn();}, 1);
    }});
    km_cmdlist['block'].cmdI = 0;
    km_cmdlist['block'].doCmdA();
  }  , loadblockdata:function(eo) {
    var bid = eo.attr('id');
    var htm = bid;
    var url = eo.data('kmHtm');
    if (String(url) == 'undefined') {url = '';}
    if (url != '') {
      htm = url;
    }else {
      km_cmdlist['block'].doCmdA();
      return;
    }
    var posting = $.ajax({type:'get', url:htm, cache:false});
    posting.done(function(data) {
      var filter = eo.data('kmFilter');
      if (String(filter) == 'undefined') {filter = '';}
      if (filter) {
        km_filter[filter](bid, data, function() {
          km_cmdlist['block'].doCmdA();
        });
      }else {
        $('#' + bid).append(data);
        km_cmdlist['block'].doCmdA();
      }
    });
    posting.fail(function(data) {
      console.log(data.responseText);
      km_cmdlist['block'].doCmdA();
    });
  }
};
function km_image(){
  var self=this;
  this.total=0;
  this.imgs=null;
  this.bar_width=0;
  this.size={};
  this.himgs={};
  this.himgs_status={};
  this.imgsUnload={};
  this.after=null;
  this.maxtimeby=20;//每张图片允许的最大加载时间(秒)
  this.it=200;//间隔时间(毫秒)
  this.its=100;//20*1000/200
  this.func=function (n,eo){
    $$("loadingline","style","width:"+Math.floor(parseFloat(n*eo.bar_width/eo.imgs.length))+"px");
    $$("loadingtxt","h","loading... "+Math.round(n*100/eo.imgs.length)+"%");
    /* after
    $$("loadingdef","h","");
    $$("loadingdef","style","display:'none'");
    */
  };
  this.init=function (){
    self.load();
    if(typeof self.after!='undefined'){
      self.timer=window.setInterval(function (){
        self.show();
      },self.it);
    }
  };
  this.load=function (){
    self.imgs={};
    self.himgs_status={};
    self.imgsUnload={};
    for(var i in this.himgs){
      this.total++;
      this.himgs_status[i]=1;
      this.imgsUnload[i]=1;
      (function (){
        var _i=i;
        self.imgs[_i]=new Image();
        self.imgs[_i].onload=function (){self.onloadImg(_i);};
        self.imgs[_i].onerror=function (){self.unloadImg(_i);};
        self.imgs[_i].onabort=function (){self.unloadImg(_i);};
        self.imgs[_i].src=self.himgs[_i];
      })();
    }
    if(this.total==0){self.after(self);return;}
  };
  this.onloadImg=function (i){
    try{
      self.size[i]={w:self.imgs[i].width,h:self.imgs[i].height};
      self.imgs[i].onload=null;
      self.imgs[i].onerror=null;
      self.imgs[i].onabort=null;
      self.himgs_status[i]=1;
      self.imgsUnload[i]=1;
      //console.log('onloadImg');
    }catch(e){
    }
  };
  this.unloadImg=function (i){
    self.size[i]={w:0,h:0};
    self.imgs[i].onload=null;
    self.imgs[i].onerror=null;
    self.imgs[i].onabort=null;
    self.himgs_status[i]=0;
    self.imgsUnload[i]=0;
    console.log('unloadImg');
  };
  this.show=function (){
    var imgNum=0;
    for (var i in self.imgs){
      if ((self.imgs[i].complete)||(self.imgsUnload[i]==0)||(self.imgsUnload[i]>self.its)){
        imgNum++;
        //console.log('imgNum: '+imgNum);
      }else{
        self.imgsUnload[i]++;
      }
    }
    self.func(imgNum,self);
    if (imgNum<self.total){return;}
    imgNum=self.total;
    window.clearInterval(self.timer);
    self.imgs=null;
    self.after(self);
  };
}
function km_frm(d, id) {
  var iframe;
  try {
    iframe = document.createElement('<iframe id="' + id + '" name="' + id + '" />');
  }catch (e) {
    iframe = document.createElement("iframe");
    var x_t = document.createAttribute("id");
    x_t.value = id;
    iframe.attributes.setNamedItem(x_t);
  }

  if ((/MSIE (6|7|8)/).test(navigator.userAgent)) {
    var x_t = document.createAttribute("frameborder");
    x_t.value = "0";
    iframe.attributes.setNamedItem(x_t);
  }else {
    iframe.frameborder = '0';
  }

  iframe.name = id;
  iframe.scrolling = 'no';

  iframe.setAttribute('width', '100%');
  iframe.setAttribute('height', '100%');
  iframe.setAttribute('scrolling', 'no');

  document.getElementById(d).appendChild(iframe);
  iframe.style.border = 'none';
  return iframe;
}
function km_cl(idx) {
  if (!(this instanceof arguments.callee)) {return new km_cl(idx);}
  var self = this;
  this.idx = idx;
  this.errorhandle = null;
  this.cmdA = [];
  this.cmdI = -1;
  this.doCmdA = function() {
    if (this.cmdI == -1) {return 0;}
    if (this.cmdI == this.cmdA.length) {this.cmdA = [];this.cmdI = -1;return 1;}
    var _n = this.cmdI;
    this.cmdI++;
    try {
      if (typeof this.cmdA[_n].func == "function") {
        var _para = (typeof this.cmdA[_n].para == "undefined") ? '' : this.cmdA[_n].para;
        this.cmdA[_n].func(_para);
      }else {

      }
    }catch (e) {
      console.log('km_cl error (' + this.idx + ') >>> ' + e.message + ', func: ' + _n);
      if (typeof this.errorhandle == 'function') {
        this.errorhandle(e.message);
      }
    }
  };
}
var km_cmdlist = {};
function km_ij(x_by, static_cmd, static_po, x_path, x_load) {
  if (typeof window['__scriptLoad'] == "undefined") {return;}

  if (typeof window['__sa'] == 'undefined') {
    window['__sa'] = [];
    for (var i in __scriptLoad) {
      window['__sa'].push(i);
    }
    window['__sa'].sort(function(a, b) {return a.localeCompare(b);});
  }

  if (x_by) {
    if (!x_path) {x_path = "";}
    var x_byA = (x_by.indexOf(",") != -1) ? x_by.split(",") : [x_by];
    var x_pathA = (x_path.indexOf(",") != -1) ? x_path.split(",") : [x_path];
    for (var j = 0; j < x_byA.length; j++) {
      if (typeof __scriptLoad[x_byA[j]] == "undefined") {
        var y_path = (typeof x_pathA[j] == "undefined") ? "" : x_pathA[j];
        __scriptLoad[x_byA[j]] = {load:false, path:y_path};
      }
    }
  }

  var static_by = "";
  for (var k = 0; k < window['__sa'].length; k++) {
    var si = window['__sa'][k];
    if (x_load == si) {__scriptLoad[si].load = true;}
    if (!__scriptLoad[si].load) {static_by = si;break;}
  }

  if (static_by == "") {
    window['__sa'] = null;
    window['__scriptLoad'] = null;
    try {
      delete window['__sa'];
      delete window['__scriptLoad'];
    }catch (e) {}
    if (typeof static_cmd != "function") {return;}
    static_cmd(static_po);
    return;
  }

  var script = document.createElement("script");
  script.type = "text/javascript";
  if (script.readyState) {//IE
    script.onreadystatechange = function() {
      if (script.readyState == "loaded" || script.readyState == "complete") {
        script.onreadystatechange = null;
        km_ij("", static_cmd, static_po, "", static_by);
      }
    };
  }else {//Others
    script.onload = function() {
      km_ij("", static_cmd, static_po, "", static_by);
    };
  }

  var f = __scriptLoad[static_by].path;
  var _v = ($proto.debug) ? sd() : $proto.public_ver;
  f = f.replace("[v]", _v);
  script.src = f;
  document.getElementsByTagName("head")[0].appendChild(script);
}
/* 表单验证 */
var km_validm = function() {
  if (!(this instanceof arguments.callee)) {return new km_validm();}
  var self = this;
  this.alert = false;
  this.pagestatus = '';
  this.formstatus = '';
  this.currField = '';
  this.clearhint = function(h) {
    var o;
    if (typeof h == 'string') {
      o = $('[name=' + h + ']');
    }else {
      o = $(h);
    }
    if (o.parent().get(0).tagName.toLowerCase() == 'label') {
      o = o.parent();
    }
    o.parent().removeClass('has-success has-error has-feedback');
    o.parent().find('.form-control-feedback').remove();
    o.parent().find('.hint').remove();
  };
  this.formcheck_h = function(_ho, r, b, _st) {
    if (_ho != null) {
      if (typeof _ho.notpass == "function") {
        _ho.notpass();
      }
    }
    sub_buttonHint(r, b, _ho, self, _st);
  };
  this.formcheck_d = function(_fields, _h, _st) {
    if (document.getElementsByName(_h).length == 0) {return true;}
    var co = document.getElementsByName(_h)[0];
    var _ho = $proto.config[_fields][_h];
    this.clearhint(_h);

    if (_ho.trim) {
      var _v_ = $.trim($(co).val());
      if (_ho.type != "file") {$(co).val(_v_);}
    }
    var _check_c = true;
    _check_c = _check_c && (_ho.m == 1);
    _check_c = _check_c && (_ho.ref != 1);
    if (_check_c) {
      var _t = _ho.t;
      if (_ho.type == '') {
        if (co.value == '' || $(co).val() == '') {self.formcheck_h(_ho, '' + _t + '为必填项', co, _st);return false;}
      }
      if (_ho.type == 'select') {
        if ((co.value.indexOf('请选择') != -1) || (co.value.indexOf('全部类型') != -1) || (co.value.indexOf('全部等级') != -1)) {
          self.formcheck_h(_ho, _t + '为必选项', co, _st);return false;
        }
        if (co.value == -1 || co.value == '') {self.formcheck_h(_ho, _t + '为必选项', co, _st);return false;}
      }
      if (_ho.type == 'checkbox' || _ho.type == 'radio') {
        if ($('[name=' + _h + ']:checked').size() == 0) {self.formcheck_h(_ho, _t + '为必选项', co, _st);return false;}
      }
    }
    if (_ho.type == '') {
      if ((co.value != '') && (_ho.extra != 1) && (co.type != 'file')) {
        var _re = null;
        var _str = '';
        if (typeof _ho.chkchar != 'undefined') {
          _re = _ho.chkchar;
          _str = _ho.t + _ho.chkcharhint;
        }else if (typeof self.chkchar != 'undefined') {
          _re = self.chkchar;
          _str = _ho.t + self.chkcharhint;
        }
        if (_re != null) {
          var _return = !_re.test(co.value);
          if (!_return) {
            self.formcheck_h(_ho, _str, co, _st);
            return false;
          }
        }
      }
    }

    if (_ho.m == 0 && co.value == '') {return true;}
    var o_cd = _ho.chk(co);
    if (!o_cd) {
      if (_ho.multi == 0) {
        self.hint('error', co, _ho.hs, _st);
      }
      return false;
    }

    if (_ho.remote != '') {
      if (!_ho.pass) {
        window[_ho.remote](_st);
        return false;
      }
    }
    self.hint('success', co, '', _st);
    return true;
  };
  this.hint = function(s, h, r, _st) {
    console.log('hint: ' + _st);
    this.clearhint(h);
    var o;
    if (typeof h == 'string') {
      o = $('[name=' + h + ']');
    }else {
      o = $(h);
    }
    if (o.parent().get(0).tagName.toLowerCase() == 'label') {
      o = o.parent();
    }
    if (s == 'success') {
      if (self.alert) {
        o.parent().addClass('has-success has-feedback');
      }else {
        o.parent().addClass('has-success has-feedback');
        o.parent().append('<span class="glyphicon glyphicon-ok form-control-feedback"></span>');
      }
    }else if (s == 'error') {
      if (self.alert) {
        o.parent().addClass('has-error has-feedback');
        Alert(r);
      }else {
        o.parent().addClass('has-error has-feedback');
        o.parent().append('<span class="glyphicon glyphicon-remove form-control-feedback"></span>');
        o.parent().append('<span class="hint animated fadeIn"><span>' + r + '</span></span>');
      }
      if (!_st) {
        $.scrollTo($(o[0]).parent());
      }
    }
  };
  this.formcheck = function(_fields, _h, _st, _func) {
    var _rf;
    if (_h) {
      _rf = self.formcheck_d(_fields, _h, _st);
      if (!_rf) {
        return false;
      }
    }else {
      var t = true;
      for (_h in $proto.config[_fields]) {
        _rf = self.formcheck_d(_fields, _h, _st);
        if (!_rf) {
          t = false;
          return false;
        }
      }
      return t;
    }
    if (typeof _func == "function") {_func();}
    return true;
  };
  this.initFields = function(_fields, sco) {
    if (typeof $proto.config[_fields] == "undefined") {return;}
    for (var f in $proto.config[_fields]) {
      if ($proto.config[_fields][f].type == 'datetime') {

      }
      if (typeof $proto.config[_fields][f]['extra'] == "undefined") {$proto.config[_fields][f]['extra'] = 0;}
      if (typeof $proto.config[_fields][f]['ref'] == 'undefined') {$proto.config[_fields][f]['ref'] = 0;}
      if (typeof $proto.config[_fields][f]['multi'] == "undefined") {$proto.config[_fields][f]['multi'] = 0;}
      if (typeof $proto.config[_fields][f]['trim'] == "undefined") {$proto.config[_fields][f]['trim'] = true;}
      if (typeof $proto.config[_fields][f]['remote'] == 'undefined') {$proto.config[_fields][f]['remote'] = '';}
      if (sco && typeof sco[f] != 'undefined') {
        var v = sco[f];
        $('[name="' + f + '"]').val(v);
      }

      (function() {
        var __fields = _fields;
        var __f = f;
        var t = $proto.config[__fields][__f].type;
        if (t != 'checkbox' && t != 'radio') {
          if ($proto.config[__fields][__f]['remote'] != '') {
            $proto.config[__fields][__f]['pass'] = false;
            $('[name="' + __f + '"]').bind('focus', function() {
              $proto.config[__fields][__f]['pass'] = false;
              self.clearhint(__f);
            });
          }else {
            console.log('bind focus');
            $('[name="' + __f + '"]').bind('focus', function() {
              console.log('clearhint');
              self.clearhint(__f);
            });
          }
          if (!self.alert) {
            $('[name="' + __f + '"]').bind('blur', function() {
              self.formcheck(__fields, __f, true);
            });
          }
        }
      })();
    }
  };
  this.initForm = function(_fields, _form, _submit, _func) {
    self.pagestatus = window.location.href;
    self.formstatus = '';
    self.currField = _fields;

    var _c = false;
    var __fields = _fields;
    self.initFields(__fields);

    self.submitconfig = {
      fields:__fields      , form:_form      , func:(typeof _func == "function") ? _func : function() {return true;}
    };
    console.log(typeof _func);

    if (_form) {
      if (document.getElementsByName(_form).length != 0) {
        document.getElementsByName(_form)[0].onsubmit = self.dosubmit;
        _c = true;
      }
    }
    if ($('#' + _submit).size() > 0 && !_c) {
      $('#' + _submit).bind('click', function() {
        self.dosubmit();
      });
    }

    try {$('input, textarea').placeholder();}catch (ex) {}
    console.log('initForm completed');
  };
  this.submitconfig = null;
  this.dosubmit = function(n) {
    console.log('dosubmit begin');
    var func = self.submitconfig.func;
    var form = self.submitconfig.form;
    var fields = self.submitconfig.fields;

    try {
      if (!self.formcheck(fields, null, null, null)) {
        self.restoreph();
        return false;
      }
    }catch (ve) {
      console.log('formcheck error: ' + ve.message);
    }

    console.log('after valid');
    try {
      if (self.formstatus == self.pagestatus) {
        self.restoreph();
        return false;
      }else {
        if (typeof $proto['formcheckhint'] == 'function') {
          if (!$proto['formcheckhint'](fields)) {
            self.restoreph();
            return false;
          }
        }
        self.formstatus = self.pagestatus;
      }
    }catch (ve) {
      console.log('formstatus error: ' + ve.message);
    }

    try {
      window.onbeforeunload = null;
      self.clearph();
    }catch (ve) {
      console.log('clearph error: ' + ve.message);
    }

    console.log('dosubmit prepare submit');

    if (!n) {
      return func();
    }else {
      if (form) {
        document.getElementsByName(form)[0].onsubmit = function() {return true;};
      }
      func();
      if (form) {
        document.getElementsByName(form)[0].submit();
        return false;//防止重复提交
      }
    }
  };
  this.restoreph = function() {
    if (typeof $proto['clickObj'] == 'undefined') {$proto.clickObj = null;}
    var _fields = self.currField;
    for (var _h in $proto.config[_fields]) {
      if (typeof $proto.config[_fields][_h].placeholder == 'undefined') {
        $proto.config[_fields][_h].placeholder = '';
      }
      if ($proto.config[_fields][_h].placeholder != '') {
        if (document.getElementsByName(_h)[0].value == '' || $proto.config[_fields][_h].placeholder == document.getElementsByName(_h)[0].value || $(document.getElementsByName(_h)[0]).val() == $proto.config[_fields][_h].placeholder) {
          if ($proto.clickObj != document.getElementsByName(_h)[0]) {
            $(document.getElementsByName(_h)[0]).val('');
          }else {
            document.getElementsByName(_h)[0].value = '';
          }
        }
      }
      if (typeof $proto.config[_fields][_h].ajax != 'undefined') {
        $proto.config[_fields][_h].ajax = false;
      }
    }
  };
  this.clearph = function() {
    var _fields = self.currField;
    for (var _h in $proto.config[_fields]) {
      if (typeof $proto.config[_fields][_h].placeholder == 'undefined') {
        $proto.config[_fields][_h].placeholder = '';
      }
      if ($proto.config[_fields][_h].placeholder != '') {
        if (document.getElementsByName(_h)[0].value == $proto.config[_fields][_h].placeholder) {
          document.getElementsByName(_h)[0].value = '';
        }
      }
    }
  };
};
function s_bsf(b) {
  try {$proto.checkfield = b;}catch (_r) {}
}
function sub_buttonHint(r, b, ho, vm, _st) {
  if (!r) {r = '';}
  try {
    vm.hint('error', $(b), r, _st);
    if (typeof b == "object") {
      s_bsf(b);
    }
  }catch (_r) {
    console.log('sub_buttonHint: ' + _r.message);
  }
}
function Alert(m_str, m_cmd, m_c, m_q) {
  if (typeof m_str == 'undefined' || !m_str) {
    $('#d_alert .modal-footer').css({display:'none'});
    $('#d_alert_body').html('');
    $('#d_alert').modal('hide');return;
  }
  if (typeof m_cmd == 'undefined' || !m_cmd) {
    m_cmd = 'Alert()';
  }
  $('#d_alert .modal-footer').css({display:'block'});
  (function() {
    var _cmd = m_cmd;
    if (!m_q) {
      if (typeof _cmd == 'string') {
        $('#d_alert .modal-footer button').unbind().bind('click', function() {
          Alert();
          $d.eval(_cmd);
        });
      }else {
        $('#d_alert .modal-footer button').unbind().bind('click', function() {
          Alert();
          _cmd();
        });
      }
    }else {
      $('#d_alert .modal-footer button').unbind().bind('click', function() {
        Alert();
      });
    }
  })();
  $('#d_alert_body').html(m_str);
  $('#d_alert').modal({backdrop:'static', keyboard:false});
}
/* 数据提交 */
var km_scr = {
  loaded:function(formname, action, target, after, clk) {
    console.log('smart:loaded');
    if (formname) {
      document.getElementsByName(formname)[0].action = action;
      document.getElementsByName(formname)[0].target = target;
    }
    //$$('frm_export').style.width='100%';
    var result = '';
    try {
      result = $.trim($('#frm_export').contents().find('body').text());
    }catch (e) {
      console.log('smart: get result error.(' + e.message + ')');
    }
    console.log('smart:result ' + result);
    //return;
    var afterback = false;
    if (typeof after == 'function') {
      console.log('smart:after');
      afterback = after(result);
    }
    $('#div_export').remove();
    if (afterback) {return;}
    if (typeof clk == 'function') {
      (function() {
        var doclk = clk;
        var _result = result;
        $('#d_alert .modal-footer button')[0].onclick = function() {
          doclk(_result);
        };
      })();
    }else {
      $('#d_alert .modal-footer button')[0].onclick = function() {
        $('#d_alert').modal('hide');
      };
    }
    $('#d_alert .modal-footer').css({display:'block'});
  }  , a:function(url, formname, before, after, clk) {
    $('#d_alert').modal();
    var x = '';
    x += '<div id="div_export" style="display:none;">';
    x += "<iframe style='display:block;' id='frm_export' name='frm_export' frameborder='0' width='1' height='1' scrolling='no' src=''></iframe>";
    x += '</div>';
    $(document.body).append(x);

    (function() {
      var _url = url;
      var _after = after;
      var _clk = clk;
      var _formname = formname;
      var _action = '';
      var _target = '';
      if (formname) {
        _action = document.getElementsByName(formname)[0].action;
        _target = document.getElementsByName(formname)[0].target;
      }
      if ($('#frm_export')[0].attachEvent) {
        $('#frm_export')[0].attachEvent("onload", function() {
          km_scr.loaded(_formname, _action, _target, _after, _clk);
        });
      }else {
        $('#frm_export')[0].onload = function() {
          km_scr.loaded(_formname, _action, _target, _after, _clk);
        };
      }
      var _done;
      if (formname) {
        _done = function() {
          console.log('smart:submit');
          document.getElementsByName(_formname)[0].method = 'post';
          document.getElementsByName(_formname)[0].action = _url;
          document.getElementsByName(_formname)[0].target = 'frm_export';
          document.getElementsByName(_formname)[0].submit();
        };
      }else {
        _done = function() {
          console.log('smart:location replace');
          if (_url.indexOf('?') == -1) {
            _url += '?_=' + sd();
          }else {
            _url += '&_=' + sd();
          }
          frm_export.location.replace(_url);
        };
      }

      if (typeof before == 'function') {
        console.log('smart:before');
        before(_done);
      }else {
        $('#d_alert_body').html('正在执行操作...');
        _done();
      }
    })();
  }
};
var km_tab = {
  clk:function(n, o, z, cb) {
    if (!cb) {cb = 'clsHide';} //clsHidden
    var ent = $d.evt();
    if (ent.stopPropagation) {
      ent.stopPropagation();
    }else {
      ent.cancelBubble = true;
    }
    if (o) {o = document.getElementById(o);}else {
      var _o = $d.elem(ent);
      if (_o.parentNode.childNodes.length > 1) {return;}
      o = _o.parentNode.parentNode.parentNode;
    }
    var _eo1 = $d.getChilds(o, "DIV", 0);
    var eo = $d.getChilds(_eo1, "SPAN", n);
    if ((_eo1 == null) || (eo == null)) {return;}
    if (eo.className.indexOf("_select") != -1) {return false;}

    var tobjs = $d.getChilds(_eo1, "SPAN");
    for (var i = 0; i < tobjs.length; i++) {
      if (tobjs[i].className.indexOf("_select") != -1) {
        var tcls = tobjs[i].className.replace("_select", "");
        $(tobjs[i]).attr('class', tcls);
      }
    }
    var t_cls = eo.className;
    if (t_cls.indexOf("_over") != -1) {
      t_cls = t_cls.replace("_over", "");
    }
    t_cls += "_select";
    $(tobjs[n]).attr('class', t_cls);
    try {if (ent.type == "mouseover") {tobjs[n].click();}}catch (p9) {alert(p9.message);}

    var _l = $d.getChilds(o, "DIV").length;
    if (z == null) {z = 1;}
    var _eo2 = $d.getChilds(o, "DIV")[_l - z];
    var tobjs0 = $d.getChilds(_eo2, "DIV");
    for (var b = 0; b < tobjs0.length; b++) {
      if ((tobjs0[b].className != cb) && (tobjs0[b].className != "d-box-bot")) {
        $(tobjs0[b]).attr('class', cb);
      }
    }
    $(tobjs0[n]).attr('class', '');
  }
};

/**/
var $d = {
  eval:function(str) {
    eval(str);
  },
  rtn:function (d){
    try{
      if(!d){return;}
      if(!d.hasChildNodes()){return;}
      var _li=d.childNodes;
      for(var _j=_li.length-1;_j>=0;_j--){
        var d_i=_li[_j];
        if((d_i.nodeType==3)&&(d_i.nodeValue.replace(/[ \f\n\r\t\v]/g,"")=="")){
          d_i.parentNode.removeChild(d_i);
        }else{
          var _d=d_i;
          $d.rtn(_d);
        }
      }
    }catch(ex){}
  },
  getWin:function(frame, doc) {
    if (PF_ie) {return frame.contentWindow;}
    if (!doc) {doc = $d.getDoc(frame);}
    if (doc.parentWindow) {
      return doc.parentWindow;
    }
    if ((isBrowser().indexOf("gecko") != -1) || (isBrowser().indexOf("safari") != -1)) {
      var scriptElement = doc.createElement('script');
      scriptElement.innerHTML = 'document.parentWindow=window';
      var parentElement = doc.documentElement;
      parentElement.appendChild(scriptElement);
      parentElement.removeChild(scriptElement);
      return doc.parentWindow;
    }
    return doc.defaultView;
  },
  getDoc:function(frame) {
    if (frame == null) {return null;}
    if ((isBrowser().indexOf("gecko") != -1) || (isBrowser().indexOf("safari") != -1)) {
      doc = (frame.document || frame.contentWindow.document);
    }else {
      doc = (frame.contentDocument || frame.contentWindow.document);
    }
    return doc;
  },
  evt:function() {
    if (document.all)return window.event;
    try {
      func = $d.evt.caller;
      while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof(arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {return arg0;}}
        func = func.caller;
      }
    }catch (er2) {return window.event;}
    return null;
  },
  elem:function(ent) {
    try {return ent.srcElement || ent.target;}catch (_em) {return null;}
  },
  dk:function(cmd) {var ent = $d.evt();if (ent.keyCode == 13) {
    if (typeof cmd == "function") {cmd();}else {eval(cmd);}
  }},
  setInterval_i:function() {
    window.addEventListener('message', function(e) {
      console.log(e.data);
    }, false);
    var it = 1000; //1s
    var iframe = document.createElement('iframe');
    iframe.style.display = 'none';
    iframe.id = 'timerios';
    //iframe.src='data:text/html,%3C%21DOCTYPE%20html%3E%0A%3Chtml%3E%0A%3Chead%3E%0A%09%3Cmeta%20charset%3D%22utf-8%22%20%2F%3E%0A%09%3Cmeta%20http-equiv%3D%22refresh%22%20content%3D%22'+it+'%22%20id%3D%22metarefresh%22%20%2F%3E%0A%09%3Ctitle%3Ex%3C%2Ftitle%3E%0A%3C%2Fhead%3E%0A%3Cbody%3E%0A%09%3Cscript%3Etop.postMessage%28%27refresh%27%2C%20%27%2A%27%29%3B%3C%2Fscript%3E%0A%3C%2Fbody%3E%0A%3C%2Fhtml%3E';
    var f = '';
    iframe.src = f;

    document.body.insertBefore(iframe, document.body.childNodes[0]);

    /*
    		var s='';
    		s+='<!doctype html>';
    		s+='<html><head>';
    		s+='<meta charset="utf-8">';
    		s+='<meta http-equiv="refresh" content="15" id="metarefresh">';
    		s+='<title>reload</title>';
    		s+='</head>';
    		s+='<body>';
    		s+='<script>top.postMessage(\'[data]\', \'*\');</script>';
    		s+='</body>';
    		s+='</html>';
    		*/

  },
  clearInterval_i:function() {
    if ($$('timerios')) {$$('timerios').parentNode.removeChild($$('timerios'));}
    //window.removeEventListener();
  },
  getChilds:function(d, tn, ni) {
    if (d == null) {return null;}
    if (!d.hasChildNodes()) {
      if (ni != null) {return null;}
      return [];
    }
    var _list = [];
    var _li = d.childNodes;
    for (var _j = 0; _j < _li.length; _j++) {
      var d_i = _li[_j];
      if (tn) {
        if (d_i.nodeName == tn.toUpperCase()) {_list.push(d_i);}
      }else {
        if ((d_i.nodeType == 3) && (d_i.nodeValue == "")) {

        }else {
          _list.push(d_i);
        }
      }
    }
    if (ni != null) {
      var _lo = null;
      try {_lo = _list[ni];}catch (ne) {}
      return _lo;
    }
    return _list;
  }
};
function sub_getPa(val, pa) {
  if ((pa == null) || (pa == "")) {pa = top.location.href;}
  if (pa.indexOf(val + "=") == -1) {return "";}
  var ppa1 = pa.split(val + "=");
  var ppa2 = (ppa1[1].indexOf('&') != -1) ? ppa1[1].split("&") : [ppa1[1]];
  return String(ppa2[0]);
}
function sub_getdate(ts, d_x) {
  var d;
  if ((ts != null) && (ts != "") && ((String(ts).indexOf("-") != -1) || (String(ts).indexOf(":") != -1) || (String(ts).indexOf(" ") != -1))) {
    d = new Date();
    var a = (ts.indexOf(" ") != -1) ? ts.split(" ") : [ts, ""];
    var b = (a[0].indexOf("-") != -1) ? a[0].split("-") : ["", "", ""];
    var c = (a[1].indexOf(":") != -1) ? a[1].split(":") : ["", "", ""];
    var s = "";
    if ((b[0] != String(d.getFullYear())) && (d_x != 2)) {s += b[0] + "-";}
    if ((b[0] == String(d.getFullYear())) && (d_x == 3)) {s += b[0] + "-";}
    s += b[1] + "-" + b[2];
    return s;
  }
  if ((ts == "today") || (ts == "") || (ts == null)) {
    d = new Date();
  }else if (ts == "yestoday") {
    var d_s = new Date().getTime() - 24 * 60 * 60 * 1000;
    d = new Date(d_s);
  }else {
    d = new Date(ts);
  }
  if (d_x == 1) {
    if (d.getFullYear() == new Date().getFullYear()) {
      return (d.getMonth() + 1) + "月" + d.getDate() + "日";
    }else {
      return d.getFullYear() + "年" + (d.getMonth() + 1) + "月" + d.getDate() + "日";
    }
  }else if (d_x == 2) {
    if (d.getFullYear() == new Date().getFullYear()) {
      return nTos(d.getMonth() + 1) + "-" + nTos(d.getDate());
    }else {
      return d.getFullYear() + "-" + nTos(d.getMonth() + 1) + "-" + nTos(d.getDate());
    }
  }else if (d_x == 3) {
    return d.getFullYear() + "年" + (d.getMonth() + 1) + "月" + d.getDate() + "日";
  }else {
    return d.getFullYear() + "-" + nTos(d.getMonth() + 1) + "-" + nTos(d.getDate());
  }
}
function sub_gettime(ts, n) {
  var d;
  if ((ts == "today") || (ts == "") || (ts == null)) {
    d = new Date();
  }else if (ts == "yestoday") {
    var d_s = new Date().getTime() - 24 * 60 * 60 * 1000;
    d = new Date(d_s);
  }else {
    d = new Date(ts);
  }
  if (n == 2) {
    return nTos(d.getHours()) + ":" + nTos(d.getMinutes());
  }else {
    return nTos(d.getHours()) + ":" + nTos(d.getMinutes()) + ":" + nTos(d.getSeconds());
  }
}
function sub_getDatetime(ts, d_x) {
  return sub_getdate(ts, d_x) + " " + sub_gettime(ts);
}
function sTon(m) {if ((m == "") || (m == null)) {return 0;}try {return parseFloat(m);}catch (_r) {return 0;}}
function nTos(m) {return (m < 10) ? "0" + String(m) : String(m);}
function nTos2(m, b) {var ss = "";if (String(m).length < b) {ss = "0";for (i = 1; i < b - String(m).length; i++) {ss += "0";}}return ss + String(m);}

function sd() {return new Date().getTime().toString() + nTos2(Math.floor(Math.random() * 1000), 4);}

function rC(cs, ic, oc) {if (cs.indexOf(ic) > -1) {cs = cs.split(ic);cs = cs.join(oc);}return cs;}
function str_trim(s) {s = s.replace(/(^\s*)|(\s*$)/g, "");return s;}
function str_trimMiddle(s) {s = s.replace(/[\f\n\r\t\v]/g, "");return s;}
function str_trimSpace(s) {s = s.replace(/(^\s*)|(\s*$)/g, "");s = s.replace(/[ \f\n\r\t\v]/g, "");return s;}
function str_trimHtml(s) {s = s.replace(/<(.*)>.*<\/\1>|<(.*) \/>/, "");return s;}
function str_trimHtml2(s) {s = s.replace(/<(.[^<]*)>/g, "");return s;}
function sub_trimValue(s) {s = str_trim(s);s = str_trimMiddle(s);s = str_trimHtml(s);return s;}
function sub_txtToHtm(s, br) {
  s = $.trim(s);
  s = s.replace(/</g, "&lt;");
  s = s.replace(/>/g, "&gt;");
  if (br) {
    s = s.replace(/\n/g, '<br>');
  }else {
    s = s.replace(/\n/g, ' ');
  }
  s = s.replace(/[\t\r]/g, '');
  return s;
}
function sub_htmToTxt(s, br) {
  s = $.trim(s);
  if (br) {
    	s = s.replace(/[\t\r]/g, '');
    s = rC(s, '<BR', '<br');
    s = rC(s, '<br>', '\n');
    s = rC(s, '<br/>', '\n');
    s = rC(s, '<br />', '\n');
  }
  s = rC(s, "&lt;", "<");
  s = rC(s, "&gt;", ">");
  return s;
}

var FileAPI_support = (function(undefined) {
  return $("<input type='file'>").get(0).files !== undefined;
})();
var PF_host = window.location.href.substring(7, window.location.href.indexOf("/", 7));
var PF_ie = false, PF_ie6 = false, PF_ie7 = false, PF_Trident = false;
function ck() {
  PF_ie = isBrowserSupported();
  if (isBrowser().indexOf("IE 版本:6") != -1) {PF_ie6 = true;}
  if (isBrowser().indexOf("IE 版本:7") != -1) {PF_ie7 = true;}
  if (isBrowser().indexOf("IE(Trident) 版本:") != -1) {PF_Trident = true;}
  try {
    var c1 = isBrowserSupported(), l = location.href;
    if ((l.indexOf("browserchk") > -1) || (l.indexOf("browsersupported") > -1)) {var cs = "BROWSER:" + isBrowser();document.getElementById("divchk").innerHTML = cs;return;}
  }catch (eb1) {}
}
function cc(f) {return f ? "支持" : "不支持";}
function isBrowserSupported() {
  var agt = navigator.userAgent.toLowerCase();
  var is_op = (agt.indexOf("opera") > -1),
  is_ie = ((agt.indexOf("msie") > -1) && document.all && !is_op),
  is_ie5 = ((agt.indexOf("msie 5") > -1) && document.all && !is_op),
  is_mac = (agt.indexOf("mac") > -1),
  is_gk = (agt.indexOf("gecko") > -1) && (agt.indexOf("trident") == -1),
  is_trident = (agt.indexOf("trident") > -1) && (agt.indexOf("msie") == -1),
  is_sf = (agt.indexOf("safari") > -1);

  var v;
  if (is_ie && !is_op && !is_mac) {
    if (agt.indexOf("palmsource") > -1 || agt.indexOf("regking") > -1 || agt.indexOf("windows ce") > -1 || agt.indexOf("j2me") > -1 || agt.indexOf("avantgo") > -1 || agt.indexOf(" stb") > -1) {
      return false;
    }
    v = GetFollowingFloat(agt, "msie ");
    if (v != null) {return (v >= 5.5);}
  }
  if (is_trident) {
    v = GetFollowingFloat(agt, "rv:");
    if (v != null) {
      return true;
    }
  }
  return false;
}
var __ie_ver = 10;
function isBrowser() {
  var s = "";
  var agt = navigator.userAgent.toLowerCase();
  //alert(agt);
  var is_op = (agt.indexOf("opera") > -1),
  is_ie = ((agt.indexOf("msie") > -1) && document.all && !is_op),
  is_ie5 = ((agt.indexOf("msie 5") > -1) && document.all && !is_op),
  is_mac = (agt.indexOf("mac") > -1),
  is_gk = (agt.indexOf("gecko") > -1) && (agt.indexOf("trident") == -1),
  is_trident = (agt.indexOf("trident") > -1) && (agt.indexOf("msie") == -1),
  is_sf = (agt.indexOf("safari") > -1);

  var v;
  if (is_ie && !is_op && !is_mac) {
    if (agt.indexOf("palmsource") > -1 || agt.indexOf("regking") > -1 || agt.indexOf("windows ce") > -1 || agt.indexOf("j2me") > -1 || agt.indexOf("avantgo") > -1 || agt.indexOf(" stb") > -1) {
      s = "非windows操作系统";
    }
    v = GetFollowingFloat(agt, "msie ");
    if (v != null) {s = "IE 版本:" + v;__ie_ver = parseFloat(v);}
  }
  if (is_trident) {
    v = GetFollowingFloat(agt, "rv:");
    if (v != null) {
      s = "IE 版本:" + v;
    }
  }
  if (is_gk && !is_sf) {
    v = GetFollowingFloat(agt, "rv:");
    if (v != null) {
      s = "gecko 版本:" + v;
    }else {
      v = GetFollowingFloat(agt, "galeon/");
      if (v != null) {s = "gecko 版本:" + v;}
    }
  }
  if (is_sf) {
    v = GetFollowingFloat(agt, "safari/");
    if (v != null) {s = "safari 版本:" + v;}
  }
  if (is_op) {
    v = GetFollowingFloat(agt, "opera ");
    if (v == null) {
      v = GetFollowingFloat(agt, "opera/");
    }
    if (v != null) {s = "Opera 版本:" + v;}
  }
  return s;
}
function GetFollowingFloat(str, pfx) {var i = str.indexOf(pfx);if (i > -1) {var v = parseFloat(str.substring(i + pfx.length));if (!isNaN(v)) {return v;}}return null;}
function isCookieEnabled() {
  var s = String(new Date().getTime());
  document.cookie = "cookie" + s;
  var ws_c = (document.cookie.indexOf("cookie" + s) > -1) ? true : false;
  document.cookie = "";
  return ws_c;
}
ck();
var devDetect = {
  isIPad:function() {return navigator.userAgent.match(/iPad/i) !== null;},
  isIPhone:function() {return navigator.userAgent.match(/iPhone/i) !== null;},
  isIOS:function() {return this.isIPhone() || this.isIPad();},
  iOSVersion:function() {
    var match = navigator.userAgent.match(/OS (\d+)_/i);
    if (match && match[1]) {return match[1];}
  },
  isIOS4:function() {
    if (!this.isIOS()) {return false;}
    var iosv = this.iOSVersion();
    return (iosv < 5 && iosv >= 4);
  }
};
window.Echo = (function(window, document, undefined) {

  'use strict';

  var store = [], offset, throttle, poll, pstore = [];

  var _inView = function(el) {
    var coords = el.getBoundingClientRect();
    return ((coords.top >= 0 && coords.left >= 0 && coords.top) <= (window.innerHeight || document.documentElement.clientHeight) + parseInt(offset));
  };

  var _pollImages = function() {
    var self1;
    for (var j = pstore.length; j--;) {
      self1 = pstore[j];
      if (_inView(self1)) {
        var ps = $(self1).data('echoPs');
        var cls = (ps.indexOf(' ') != -1) ? ps.split(' ') : [ps];
        for (var k = 0; k < cls.length; k++) {
          if (PF_ie && __ie_ver < 10) {
            $(self1).css({opacity:1});
          }else {
            $(self1).addClass(cls[k]);
          }
        }
        pstore.splice(j, 1);
      }
    }
    var self;
    for (var i = store.length; i--;) {
      self = store[i];
      if (_inView(self)) {

        var a = [];
        if ($(self).data('box')) {
          var box = $(self).data('box');
          if (box.indexOf(',') != -1) {
            a = box.split(',');
            a[0] = parseFloat(a[0]);
            a[1] = parseFloat(a[1]);
          }
        }
        if ($(self).data('showimage')) {
          var showimage = $(self).data('showimage');
          window[showimage](self);
        }else if (a.length > 0) {
          _showImageBox(self, a);
        }else {
          _showImage(self);
        }

        var echoType = $(self).data('echoType');
        if (echoType == 'expand') {
          var w = self.parentNode.offsetWidth;
          var h = self.parentNode.offsetHeight;
          $(self).animate({
            width:w , height:h
          }, 500, function() {

          });
        }

        store.splice(i, 1);
      }
    }
    if (typeof window['afterEcho'] != 'undefined') {
      window['afterEcho']();
    }
  };

  var _showImage = function(self) {
    (function() {
      var _self = self;
      var img = new Image();
      img.onload = function() {
        _self.src = $(_self).data('echo');
        img.onload = null;
        img = null;
      };
      if ($(_self).data('error')) {
        img.onerror = function() {
          if ($(_self).data('error').indexOf($(_self).attr('src')) == -1) {
            img.onload = null;
            img = null;
            $(_self).attr('src', $(_self).data('error'));
          }
        };
      }
      img.src = $(_self).data('echo');
    })();
  };

  var _showImageBox = function(self, a) {
    (function() {

      var _self = self;
      var _a = a;
      var img = new Image();
      img.onload = function() {
        var _w = img.width;
        var _h = img.height;
        var b = km_image_util.resize(_w, _h, _a[0], _a[1]);
        //console.log('a: '+a[0]+'|'+a[1]);
        //console.log('b: '+b[0]+'|'+b[1]);

        var _l = 0, _t = 0;
        if (_w > _a[0]) {
          _l = 0 - (b[0] - _a[0]) / 2;
        }
        if (_h > _a[1]) {
          _t = 0 - (b[1] - _a[1]) / 2;
        }

        $(_self).css({
          'position':'absolute' , 'top':_t , 'left':_l , 'width':b[0] + 'px' , 'height':b[1] + 'px'
        });

        _self.src = $(_self).data('echo');

        img.onload = null;
        img = null;

      };
      if ($(_self).data('error')) {
        img.onerror = function() {
          if ($(_self).data('error').indexOf($(_self).attr('src')) == -1) {
            img.onload = null;
            img = null;
            $(_self).attr('src', $(_self).data('error'));
          }
        };
      }
      img.src = $(_self).data('echo');

    })();

  };

  var _throttle = function() {
    clearTimeout(poll);
    poll = setTimeout(_pollImages, throttle);
  };

  var init = function(obj) {
    /*
    $('img[data-echo]').each(function (i){
        if($(this).data('error')){
          $(this).on('error',function (){
            if($(this).data('error').indexOf($(this).attr('src'))==-1){
              $(this).attr('src',$(this).data('error'));
            }
          });
        }
    });
    */
    var nodes = $('img[data-echo]');
    var psnodes = $('[data-echo-ps]');
    var opts = obj || {};
    offset = opts.offset || 0;
    throttle = opts.throttle || 250;

    for (var i = 0; i < nodes.length; i++) {
      store.push(nodes[i]);
    }
    for (var j = 0; j < psnodes.length; j++) {
      pstore.push(psnodes[j]);
    }

    _throttle();

    if (document.addEventListener) {
      window.addEventListener('scroll', _throttle, false);
    } else {
      window.attachEvent('onscroll', _throttle);
    }
  };

  return {
    init: init,
    render: _throttle
  };

})(window, document);


/* Copyright (c) 2010-2013 Marcus Westin */
(function(e){function o(){try{return r in e&&e[r]}catch(t){return!1}}var t={},n=e.document,r="localStorage",i="script",s;t.disabled=!1,t.set=function(e,t){},t.get=function(e){},t.remove=function(e){},t.clear=function(){},t.transact=function(e,n,r){var i=t.get(e);r==null&&(r=n,n=null),typeof i=="undefined"&&(i=n||{}),r(i),t.set(e,i)},t.getAll=function(){},t.forEach=function(){},t.serialize=function(e){return JSON.stringify(e)},t.deserialize=function(e){if(typeof e!="string")return undefined;try{return JSON.parse(e)}catch(t){return e||undefined}};if(o())s=e[r],t.set=function(e,n){return n===undefined?t.remove(e):(s.setItem(e,t.serialize(n)),n)},t.get=function(e){return t.deserialize(s.getItem(e))},t.remove=function(e){s.removeItem(e)},t.clear=function(){s.clear()},t.getAll=function(){var e={};return t.forEach(function(t,n){e[t]=n}),e},t.forEach=function(e){for(var n=0;n<s.length;n++){var r=s.key(n);e(r,t.get(r))}};else if(n.documentElement.addBehavior){var u,a;try{a=new ActiveXObject("htmlfile"),a.open(),a.write("<"+i+">document.w=window</"+i+'><iframe src="/favicon.ico"></iframe>'),a.close(),u=a.w.frames[0].document,s=u.createElement("div")}catch(f){s=n.createElement("div"),u=n.body}function l(e){return function(){var n=Array.prototype.slice.call(arguments,0);n.unshift(s),u.appendChild(s),s.addBehavior("#default#userData"),s.load(r);var i=e.apply(t,n);return u.removeChild(s),i}}var c=new RegExp("[!\"#$%&'()*+,/\\\\:;<=>?@[\\]^`{|}~]","g");function h(e){return e.replace(/^d/,"___$&").replace(c,"___")}t.set=l(function(e,n,i){return n=h(n),i===undefined?t.remove(n):(e.setAttribute(n,t.serialize(i)),e.save(r),i)}),t.get=l(function(e,n){return n=h(n),t.deserialize(e.getAttribute(n))}),t.remove=l(function(e,t){t=h(t),e.removeAttribute(t),e.save(r)}),t.clear=l(function(e){var t=e.XMLDocument.documentElement.attributes;e.load(r);for(var n=0,i;i=t[n];n++)e.removeAttribute(i.name);e.save(r)}),t.getAll=function(e){var n={};return t.forEach(function(e,t){n[e]=t}),n},t.forEach=l(function(e,n){var r=e.XMLDocument.documentElement.attributes;for(var i=0,s;s=r[i];++i)n(s.name,t.deserialize(e.getAttribute(s.name)))})}try{var p="__storejs__";t.set(p,p),t.get(p)!=p&&(t.disabled=!0),t.remove(p)}catch(f){t.disabled=!0}t.enabled=!t.disabled,typeof module!="undefined"&&module.exports&&this.module!==module?module.exports=t:typeof define=="function"&&define.amd?define(t):e.store=t})(Function("return this")())
function isLocalStorageNameSupported(){
	var testKey='test';
	try{
		store.set(testKey,'t');
		var _r=(store.get(testKey)=='t');
		store.remove(testKey);
		return _r;
	}catch(error){
		return false;
	}
}
var __isLocalStorageNameSupported=isLocalStorageNameSupported();

/*! http://mths.be/placeholder v2.0.7 by @mathias */
;(function(f,h,$){var a='placeholder' in h.createElement('input'),d='placeholder' in h.createElement('textarea'),i=$.fn,c=$.valHooks,k,j;if(a&&d){j=i.placeholder=function(){return this};j.input=j.textarea=true}else{j=i.placeholder=function(){var l=this;l.filter((a?'textarea':':input')+'[placeholder]').not('.placeholder').bind({'focus.placeholder':b,'blur.placeholder':e}).data('placeholder-enabled',true).trigger('blur.placeholder');return l};j.input=a;j.textarea=d;k={get:function(m){var l=$(m);return l.data('placeholder-enabled')&&l.hasClass('placeholder')?'':m.value},set:function(m,n){var l=$(m);if(!l.data('placeholder-enabled')){return m.value=n}if(n==''){m.value=n;if(m!=h.activeElement){e.call(m)}}else{if(l.hasClass('placeholder')){b.call(m,true,n)||(m.value=n)}else{m.value=n}}return l}};a||(c.input=k);d||(c.textarea=k);$(function(){$(h).delegate('form','submit.placeholder',function(){var l=$('.placeholder',this).each(b);setTimeout(function(){l.each(e)},10)})});$(f).bind('beforeunload.placeholder',function(){$('.placeholder').each(function(){this.value=''})})}function g(m){var l={},n=/^jQuery\d+$/;$.each(m.attributes,function(p,o){if(o.specified&&!n.test(o.name)){l[o.name]=o.value}});return l}function b(m,n){var l=this,o=$(l);if(l.value==o.attr('placeholder')&&o.hasClass('placeholder')){if(o.data('placeholder-password')){o=o.hide().next().show().attr('id',o.removeAttr('id').data('placeholder-id'));if(m===true){return o[0].value=n}o.focus()}else{l.value='';o.removeClass('placeholder');l==h.activeElement&&l.select()}}}function e(){var q,l=this,p=$(l),m=p,o=this.id;if(l.value==''){if(l.type=='password'){if(!p.data('placeholder-textinput')){try{q=p.clone().attr({type:'text'})}catch(n){q=$('<input>').attr($.extend(g(this),{type:'text'}))}q.removeAttr('name').data({'placeholder-password':true,'placeholder-id':o}).bind('focus.placeholder',b);p.data({'placeholder-textinput':q,'placeholder-id':o}).before(q)}p=p.removeAttr('id').hide().prev().attr('id',o).show()}p.addClass('placeholder');p[0].value=p.attr('placeholder')}else{p.removeClass('placeholder')}}}(this,document,jQuery));

/**
 * Copyright (c) 2007-2015 Ariel Flesler - aflesler<a>gmail<d>com | http://flesler.blogspot.com
 * Licensed under MIT
 * @author Ariel Flesler
 * @version 2.1.1
 */
;(function(f){"use strict";"function"===typeof define&&define.amd?define(["jquery"],f):"undefined"!==typeof module&&module.exports?module.exports=f(require("jquery")):f(jQuery)})(function($){"use strict";function n(a){return!a.nodeName||-1!==$.inArray(a.nodeName.toLowerCase(),["iframe","#document","html","body"])}function h(a){return $.isFunction(a)||$.isPlainObject(a)?a:{top:a,left:a}}var p=$.scrollTo=function(a,d,b){return $(window).scrollTo(a,d,b)};p.defaults={axis:"xy",duration:0,limit:!0};$.fn.scrollTo=function(a,d,b){"object"=== typeof d&&(b=d,d=0);"function"===typeof b&&(b={onAfter:b});"max"===a&&(a=9E9);b=$.extend({},p.defaults,b);d=d||b.duration;var u=b.queue&&1<b.axis.length;u&&(d/=2);b.offset=h(b.offset);b.over=h(b.over);return this.each(function(){function k(a){var k=$.extend({},b,{queue:!0,duration:d,complete:a&&function(){a.call(q,e,b)}});r.animate(f,k)}if(null!==a){var l=n(this),q=l?this.contentWindow||window:this,r=$(q),e=a,f={},t;switch(typeof e){case "number":case "string":if(/^([+-]=?)?\d+(\.\d+)?(px|%)?$/.test(e)){e= h(e);break}e=l?$(e):$(e,q);if(!e.length)return;case "object":if(e.is||e.style)t=(e=$(e)).offset()}var v=$.isFunction(b.offset)&&b.offset(q,e)||b.offset;$.each(b.axis.split(""),function(a,c){var d="x"===c?"Left":"Top",m=d.toLowerCase(),g="scroll"+d,h=r[g](),n=p.max(q,c);t?(f[g]=t[m]+(l?0:h-r.offset()[m]),b.margin&&(f[g]-=parseInt(e.css("margin"+d),10)||0,f[g]-=parseInt(e.css("border"+d+"Width"),10)||0),f[g]+=v[m]||0,b.over[m]&&(f[g]+=e["x"===c?"width":"height"]()*b.over[m])):(d=e[m],f[g]=d.slice&& "%"===d.slice(-1)?parseFloat(d)/100*n:d);b.limit&&/^\d+$/.test(f[g])&&(f[g]=0>=f[g]?0:Math.min(f[g],n));!a&&1<b.axis.length&&(h===f[g]?f={}:u&&(k(b.onAfterFirst),f={}))});k(b.onAfter)}})};p.max=function(a,d){var b="x"===d?"Width":"Height",h="scroll"+b;if(!n(a))return a[h]-$(a)[b.toLowerCase()]();var b="client"+b,k=a.ownerDocument||a.document,l=k.documentElement,k=k.body;return Math.max(l[h],k[h])-Math.min(l[b],k[b])};$.Tween.propHooks.scrollLeft=$.Tween.propHooks.scrollTop={get:function(a){return $(a.elem)[a.prop]()}, set:function(a){var d=this.get(a);if(a.options.interrupt&&a._last&&a._last!==d)return $(a.elem).stop();var b=Math.round(a.now);d!==b&&($(a.elem)[a.prop](b),a._last=this.get(a))}};return p});

/* mustache */
(function defineMustache(global,factory){if(typeof exports==="object"&&exports){factory(exports)}else if(typeof define==="function"&&define.amd){define(["exports"],factory)}else{global.Mustache={};factory(Mustache)}})(this,function mustacheFactory(mustache){var objectToString=Object.prototype.toString;var isArray=Array.isArray||function isArrayPolyfill(object){return objectToString.call(object)==="[object Array]"};function isFunction(object){return typeof object==="function"}function escapeRegExp(string){return string.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g,"\\$&")}function hasProperty(obj,propName){return obj!=null&&typeof obj==="object"&&propName in obj}var regExpTest=RegExp.prototype.test;function testRegExp(re,string){return regExpTest.call(re,string)}var nonSpaceRe=/\S/;function isWhitespace(string){return!testRegExp(nonSpaceRe,string)}var entityMap={"&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#39;","/":"&#x2F;"};function escapeHtml(string){return String(string).replace(/[&<>"'\/]/g,function fromEntityMap(s){return entityMap[s]})}var whiteRe=/\s*/;var spaceRe=/\s+/;var equalsRe=/\s*=/;var curlyRe=/\s*\}/;var tagRe=/#|\^|\/|>|\{|&|=|!/;function parseTemplate(template,tags){if(!template)return[];var sections=[];var tokens=[];var spaces=[];var hasTag=false;var nonSpace=false;function stripSpace(){if(hasTag&&!nonSpace){while(spaces.length)delete tokens[spaces.pop()]}else{spaces=[]}hasTag=false;nonSpace=false}var openingTagRe,closingTagRe,closingCurlyRe;function compileTags(tagsToCompile){if(typeof tagsToCompile==="string")tagsToCompile=tagsToCompile.split(spaceRe,2);if(!isArray(tagsToCompile)||tagsToCompile.length!==2)throw new Error("Invalid tags: "+tagsToCompile);openingTagRe=new RegExp(escapeRegExp(tagsToCompile[0])+"\\s*");closingTagRe=new RegExp("\\s*"+escapeRegExp(tagsToCompile[1]));closingCurlyRe=new RegExp("\\s*"+escapeRegExp("}"+tagsToCompile[1]))}compileTags(tags||mustache.tags);var scanner=new Scanner(template);var start,type,value,chr,token,openSection;while(!scanner.eos()){start=scanner.pos;value=scanner.scanUntil(openingTagRe);if(value){for(var i=0,valueLength=value.length;i<valueLength;++i){chr=value.charAt(i);if(isWhitespace(chr)){spaces.push(tokens.length)}else{nonSpace=true}tokens.push(["text",chr,start,start+1]);start+=1;if(chr==="\n")stripSpace()}}if(!scanner.scan(openingTagRe))break;hasTag=true;type=scanner.scan(tagRe)||"name";scanner.scan(whiteRe);if(type==="="){value=scanner.scanUntil(equalsRe);scanner.scan(equalsRe);scanner.scanUntil(closingTagRe)}else if(type==="{"){value=scanner.scanUntil(closingCurlyRe);scanner.scan(curlyRe);scanner.scanUntil(closingTagRe);type="&"}else{value=scanner.scanUntil(closingTagRe)}if(!scanner.scan(closingTagRe))throw new Error("Unclosed tag at "+scanner.pos);token=[type,value,start,scanner.pos];tokens.push(token);if(type==="#"||type==="^"){sections.push(token)}else if(type==="/"){openSection=sections.pop();if(!openSection)throw new Error('Unopened section "'+value+'" at '+start);if(openSection[1]!==value)throw new Error('Unclosed section "'+openSection[1]+'" at '+start)}else if(type==="name"||type==="{"||type==="&"){nonSpace=true}else if(type==="="){compileTags(value)}}openSection=sections.pop();if(openSection)throw new Error('Unclosed section "'+openSection[1]+'" at '+scanner.pos);return nestTokens(squashTokens(tokens))}function squashTokens(tokens){var squashedTokens=[];var token,lastToken;for(var i=0,numTokens=tokens.length;i<numTokens;++i){token=tokens[i];if(token){if(token[0]==="text"&&lastToken&&lastToken[0]==="text"){lastToken[1]+=token[1];lastToken[3]=token[3]}else{squashedTokens.push(token);lastToken=token}}}return squashedTokens}function nestTokens(tokens){var nestedTokens=[];var collector=nestedTokens;var sections=[];var token,section;for(var i=0,numTokens=tokens.length;i<numTokens;++i){token=tokens[i];switch(token[0]){case"#":case"^":collector.push(token);sections.push(token);collector=token[4]=[];break;case"/":section=sections.pop();section[5]=token[2];collector=sections.length>0?sections[sections.length-1][4]:nestedTokens;break;default:collector.push(token)}}return nestedTokens}function Scanner(string){this.string=string;this.tail=string;this.pos=0}Scanner.prototype.eos=function eos(){return this.tail===""};Scanner.prototype.scan=function scan(re){var match=this.tail.match(re);if(!match||match.index!==0)return"";var string=match[0];this.tail=this.tail.substring(string.length);this.pos+=string.length;return string};Scanner.prototype.scanUntil=function scanUntil(re){var index=this.tail.search(re),match;switch(index){case-1:match=this.tail;this.tail="";break;case 0:match="";break;default:match=this.tail.substring(0,index);this.tail=this.tail.substring(index)}this.pos+=match.length;return match};function Context(view,parentContext){this.view=view;this.cache={".":this.view};this.parent=parentContext}Context.prototype.push=function push(view){return new Context(view,this)};Context.prototype.lookup=function lookup(name){var cache=this.cache;var value;if(cache.hasOwnProperty(name)){value=cache[name]}else{var context=this,names,index,lookupHit=false;while(context){if(name.indexOf(".")>0){value=context.view;names=name.split(".");index=0;while(value!=null&&index<names.length){if(index===names.length-1)lookupHit=hasProperty(value,names[index]);value=value[names[index++]]}}else{value=context.view[name];lookupHit=hasProperty(context.view,name)}if(lookupHit)break;context=context.parent}cache[name]=value}if(isFunction(value))value=value.call(this.view);return value};function Writer(){this.cache={}}Writer.prototype.clearCache=function clearCache(){this.cache={}};Writer.prototype.parse=function parse(template,tags){var cache=this.cache;var tokens=cache[template];if(tokens==null)tokens=cache[template]=parseTemplate(template,tags);return tokens};Writer.prototype.render=function render(template,view,partials){var tokens=this.parse(template);var context=view instanceof Context?view:new Context(view);return this.renderTokens(tokens,context,partials,template)};Writer.prototype.renderTokens=function renderTokens(tokens,context,partials,originalTemplate){var buffer="";var token,symbol,value;for(var i=0,numTokens=tokens.length;i<numTokens;++i){value=undefined;token=tokens[i];symbol=token[0];if(symbol==="#")value=this.renderSection(token,context,partials,originalTemplate);else if(symbol==="^")value=this.renderInverted(token,context,partials,originalTemplate);else if(symbol===">")value=this.renderPartial(token,context,partials,originalTemplate);else if(symbol==="&")value=this.unescapedValue(token,context);else if(symbol==="name")value=this.escapedValue(token,context);else if(symbol==="text")value=this.rawValue(token);if(value!==undefined)buffer+=value}return buffer};Writer.prototype.renderSection=function renderSection(token,context,partials,originalTemplate){var self=this;var buffer="";var value=context.lookup(token[1]);function subRender(template){return self.render(template,context,partials)}if(!value)return;if(isArray(value)){for(var j=0,valueLength=value.length;j<valueLength;++j){buffer+=this.renderTokens(token[4],context.push(value[j]),partials,originalTemplate)}}else if(typeof value==="object"||typeof value==="string"||typeof value==="number"){buffer+=this.renderTokens(token[4],context.push(value),partials,originalTemplate)}else if(isFunction(value)){if(typeof originalTemplate!=="string")throw new Error("Cannot use higher-order sections without the original template");value=value.call(context.view,originalTemplate.slice(token[3],token[5]),subRender);if(value!=null)buffer+=value}else{buffer+=this.renderTokens(token[4],context,partials,originalTemplate)}return buffer};Writer.prototype.renderInverted=function renderInverted(token,context,partials,originalTemplate){var value=context.lookup(token[1]);if(!value||isArray(value)&&value.length===0)return this.renderTokens(token[4],context,partials,originalTemplate)};Writer.prototype.renderPartial=function renderPartial(token,context,partials){if(!partials)return;var value=isFunction(partials)?partials(token[1]):partials[token[1]];if(value!=null)return this.renderTokens(this.parse(value),context,partials,value)};Writer.prototype.unescapedValue=function unescapedValue(token,context){var value=context.lookup(token[1]);if(value!=null)return value};Writer.prototype.escapedValue=function escapedValue(token,context){var value=context.lookup(token[1]);if(value!=null)return mustache.escape(value)};Writer.prototype.rawValue=function rawValue(token){return token[1]};mustache.name="mustache.js";mustache.version="2.1.2";mustache.tags=["{{","}}"];var defaultWriter=new Writer;mustache.clearCache=function clearCache(){return defaultWriter.clearCache()};mustache.parse=function parse(template,tags){return defaultWriter.parse(template,tags)};mustache.render=function render(template,view,partials){return defaultWriter.render(template,view,partials)};mustache.to_html=function to_html(template,view,partials,send){var result=mustache.render(template,view,partials);if(isFunction(send)){send(result)}else{return result}};mustache.escape=escapeHtml;mustache.Scanner=Scanner;mustache.Context=Context;mustache.Writer=Writer});
