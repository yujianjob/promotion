var $proto = {
  pageloaded: false,
  init:function(fn) {
    $proto.pageLoad=(typeof fn == 'undefined') ? function (){} : fn;
    km_cmdlist['init'] = new km_cl('init');
    km_cmdlist['init'].cmdA.push({func:function() {
      if (typeof window['__scriptLoad'] == 'undefined') { window['__scriptLoad'] = {}; }
      if(location.href.indexOf('127.0.0.1')!=-1){
        __scriptLoad['livereload']={path:'/vendor/livereload.js'};
      }
      km_ij('', function() { km_cmdlist['init'].doCmdA(); });
    }});
    km_cmdlist['init'].cmdA.push({func:function() {
        $(document.body).append('<div id="d_alert" data-km-htm="/Resources/UI/blk_alert.html" class="modal" role="dialog"></div>');
      km_jc.loadblock(function() { km_cmdlist['init'].doCmdA(); });
    }});
    km_cmdlist['init'].cmdA.push({func:function() {
      if (Echo) {
        Echo.init({
          offset: 100,
          throttle: 150
        });
        Echo.render();
      }
      km_cmdlist['init'] = null;
      try {delete km_cmdlist['init'];}catch (e) {}
      //$d.rtn(document.body);
      $proto.pageloaded = true;
      $proto.pageLoad();
    }});
    km_cmdlist['init'].cmdI = 0;
    km_cmdlist['init'].doCmdA();
  }
};
