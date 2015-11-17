using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace Site.Code
{
    public class TempMission
    {
        static TempMission me = new TempMission();
        public static TempMission Instance
        {
            get { return me; }
            set { me = value; }
        }
        #region
        List<string> ranName = new List<string>() { "小鱼", "豆豆鱼", "水中的鱼", "鱼尾巴", "蓝色双鱼", "想飞的鱼", "爱飞的鱼", "嘟嘟鱼", "溺水的鱼", "淹死的鱼oO", "流浪的鱼", "爱哭的鱼", "睡在树上的鱼", "海河里的鱼", "岸上鱼", "冷冰鱼", "章小鱼", "酷鱼", "半死鱼", "嘉楠鱼", "蝈蝈鱼", "黑鱼崽", "深海的鱼", "站在岸上的鱼", "飞奔的鱼", "丢了翅膀的鱼", "其名为鲲。逆鳞", "与世隔绝的鱼", "夜行神鱼", "飞鱼", "翻车鱼", "鱼啊鱼", "小鲜鱼", "白鲨", "鲸鱼", "水瓶鲸鱼", "海怪", "海星", "瘦死的骆驼", "大傻兔", "雪兔", "兔哥", "流口水的兔子", "披着狼皮的兔子", "精灵鼠小妹", "汤耗子", "戴小鼠", "小老鼠", "冰松鼠", "耀华河马", "小鳄鱼", "良驹", "快马", "嘟嘟马", "大青马", "妖狐藏马", "小狗", "老狗", "北方的狗", "想飞的鸭子", "可乐鸭", "刺猬小可爱", "刺猥", "小飞象", "小鸡", "小牛", "小虾米", "寒蟾", "麋鹿", "北方的蛇", "金蛇君", "鸭一嘴", "皮皮虾", "猴子", "一只小毛驴", "王八", "秋风", "远风", "栩栩清风", "竹影清风", "夜风", "寒风", "天堂的风", "南来风", "风继续吹", "风之引力", "海上追风", "蓝风", "萧风", "张狂的风", "风在云颠", "流浪的疾风", "清风", "天风", "沉默风", "东野牧风", "黑狐无风", "牧野静风", "淳风", "舜风", "朔风", "自在的风", "往日随风", "自由如风", "爱晒太阳的风", "淡淡的风", "笑凌风", "秋风", "远风", "栩栩清风", "竹影清风", "夜风", "寒风", "天堂的风", "南来风", "风继续吹", "风之引力", "海上追风", "蓝风", "萧风", "张狂的风", "风在云颠", "流浪的疾风", "清风", "天风", "沉默风", "东野牧风", "黑狐无风", "牧野静风", "淳风", "舜风", "朔风", "自在的风", "往日随风", "自由如风", "爱晒太阳的风", "淡淡的风", "笑凌风", "萍萍", "雅婷", "雅雅", "雅楠", "雅雯", "稚雅", "苏珊", "海琳", "安琪儿", "晓瑷", "容容", "晶莹", "清水美人", "黛儿", "紫璇", "美子", "梦如", "楚楚", "紫嫣", "娴雨婷", "梦晶", "静伊", "彤彤", "小菲", "馨儿微安", "简单萱萱", "诗婕", "云馨", "相奈儿", "絮儿", "颦儿", "蓝齐儿", "碧珊", "雨婷雨婷", "茜茜", "明慧", "东美", "雪婷", "洁娜kina", "kobe菲菲", "雨莹", "反方向的钟", "电灯泡", "痰盂", "板凳", "簸箕簸箕", "蓝色手表", "窗帘", "刀片", "口红", "买女孩的小火柴", "铁锤", "小榔头", "寂寞的cd机", "童心小镜子", "水桶", "救生圈", "弹簧", "发动机V8", "机器零件", "黑白天平", "罐子", "沙漏", "透明罐子", "蓝色香水瓶", "铃铛", "红铃铛", "风铃", "玻璃耗子", "水晶叶子", "石头.剪刀.布", "伤口上的盐", "玩具", "发条兔子", "火花7588", "花心筒", "木墩", "唱片", "外挂滤镜", "哈哈镜", "哭泣的键盘", "黄书包", "纸菊花", "塑料荷花", "水之印", "蜡烛", "千千结", "烟花", "冰箱^", "猫眼", "双色猫眼se", "天样纸", "红丝砚", "吧吧炉", "水啊水", "伊水", "秋水", "春江水", "上善若水", "水澜", "流水", "行云流水", "绿水悠悠", "幽幽蓝水", "似水", "流动的水", "凌乱如水加勒比海蓝", "振海", "舒适海", "文海", "云海", "风影海", "广海", "望海", "天堂海", "平镜海岸", "伤心太平洋", "百里溪", "兰溪", "溪乐", "湘江2001", "我爱黄河", "南河", "香山", "乔山", "大雪山", "金山", "江山", "爱苍山", "枫林火山", "丘岳", "唐晨峰", "宝剑峰", "梦想之巅", "梦海之巅", "江城子", "极地", "海内比邻", "料峭", "峭壁", "流泪谷", "死亡谷", "鬼谷幽道", "原野", "晓野", "杨柳岸", "池塘边", "兰汀", "孤岛", "怪岛", "双桥", "多情沙滩", "迷路的地图", "天涯海角", "沙漠超", "滨江大道东", "一品泉", "源泉", "潭深深", "凡高的麦田", "www.菜地.com", "鼓浪屿", "蒋南亚", "北国风光", "云和山的彼端", "雨界", "懿州", "神州行", "纳木错", "橙色荷兰", "漠河", "承德露露", "英格兰", "黎巴嫩", "麦嘉", "cccp", "张大红", "张无忌", "奕柯", "李拜四", "欧阳费劲", "东方消沉", "张大民", "张二民", "高天乐", "朱维妙", "范哲", "范峻", "孙益申", "宋晓培", "苏菲", "梁婉婷", "吴逸", "吴雨", "陈良昱", "凌无卿", "魏风华", "马可可", "周鼎", "周子安", "坤哥9597", "任莹莹", "刘占宇", "崔元晖", "王小柔", "凌昕", "梁婉婷", "杨建雄", "小文", "小冉", "子安", "陈西", "老杰", "老明", "杨胖胖", "李文", "李剑", "李霖", "许锰", "刘克谦", "东方聊天", "爱新觉罗静静", "令狐帅帅", "上官小仙", "唐僧gg", "八戒", "猪小戒", "悟空", "车寅次郎", "公子襄", "晏子", "苏武", "勾践", "轩辕黄帝", "炎帝", "炫烨", "虹一法师", "本拉登", "缪斯", "查拉图斯特拉", "苏格拉底", "亨利八世", "莎士比亚", "拿破伦二世", "道拉格斯", "奥力芙", "致命朱丽叶", "卡内基", "摩西", "乔丹", "巴乔", "巴蒂1988", "王治郅", "李珊", "施连志", "张效瑞", "老亮", "帅根伟", "卡西莫多", "骑天大圣", "西门庆", "林妹妹lucklili", "华安", "喜儿", "周润发", "金庸", "好兵帅克","孔曰成仁","woro","zhni"," 幽篁晓筑","  小*你","小滴","灵羽扬","   阅微","古墓贞","  紫色偶然","  澄之自由"," 暴力","花葬","紫牧","风影","习子","  君莹天下"," 小费"," 朵朵","  烛光","  知雅意"," 死舞天葬","超频"," 佳薪","真朋友","泡沫呼吸","新概念","  秋卡"," 仙水忍","  消亡","  炊烟","  大道无言","  糖恩","  花舞语","  潺潺sw","  中国电信","  中国移动"," 秘密 ","亓亓"," 堕天翼","黑色幽默"," 禁止乖张"," 留影","  门跌塌","  金骨实","  风过留情","  你我他","   非来非去"," 假如是一种偶然","重感情的男人wu"," 冻死我也要光着腿","  天使不相信哭泣","不想再让天使哭泣 ","我的世界因你而精彩"," 天亮说晚安"," 城建学院乱收费","   结局接近开始","   最特别的存在","  生活所以堕落","  雪的那一边","  失恋的人是可耻的","  孤独杀手不怕冷","  人生如梦似过客","  水儿响叮当","  当一切成为可能","  怀念声名狼籍的日子","  百花同盟之解散 "," 才华惊动党中央 ","  鸡蛋碰石头","  毁容还是帅","   掌心上的星光","  当一切成为可能","  ","哪边花更香","冷傲的化装","小拇指上的纯银戒指","   当阳光投到水中","  木村之友伟媛","  你是风儿我是沙","笑看风云淡","  引刀成一笑","  过了保质期","   仗剑少年游","  夜色也浪漫"," 荒原的风铃","失败是成功知母","过了保质期","与魔鬼共骑","  你快乐我随意","  一半是火焰"," 阿龙"," 阿力"," 阿萌319","阿宝","阿旺","  阿king","  阿玲0525","  阿鹏","  阿司","  阿宪","  阿强","  啊庆","阿朱","阿菜","  阿Nam ","阿颖","阿韬","阿凉.吴","阿巍","  阿砰","  阿甘","  阿全","  阿文","    阿benn","蚂蚁阿德","  爱情余味","淡淡的味道","  幽兰气息","  浓重烟草味","绿茶香氛","  真水无香","檀香","  苦瓜香","书香气"," 三天晒网","  北方网"," 曾氏六合网","  南方网","中国长城网","  雨帆 "," 红色风帆","  蒂帆","  西门在线","  疯狂热线"," 花旗"," 击水三千" };
        #endregion
        /// <summary>
        /// 为某个活动添加用户。新增新的用户。
        /// </summary>
        /// <param name="actId"></param>
        /// <param name="memberCount"></param>
        public void AddMemberToAct(int actId, int memberCount)
        {
            DataSet ds = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteDataSetBySql("select top " + memberCount.ToString() + " * from member_account order by newid()");
          Random rd = new Random();
          int i = rd.Next(1, 100);
          object obj;
          string username, nickname,suiji;
        
          foreach (DataRow row in ds.Tables[0].Rows)
          {
              suiji=(rd.Next(1965,2000)).ToString ();
               
                bool isFinsh = false;

                while (!isFinsh)
                {
                    username = row["username"].ToString() + suiji;
                    nickname = ranName[rd.Next(0, ranName.Count - 1)];
                    obj = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteScalarBySql("select count(1) from member_account where username='" + username + "' or nickname='" + nickname + "'");
                    if (obj != null && int.Parse(obj.ToString()) <= 0)
                    {
                        obj = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteScalarBySql("INSERT INTO [Member_Account]([UserName],[Password] ,[Status],[Pic],[Nickname],[IsNeedReName],email) values ('" + username + "','CE0BFD15059B68D67688884D7A3D3E8C',1,'" + row["pic"].ToString() + "','" + nickname + "','1','testEmail@test.com');select @@identity;");
                        int uid;
                        if (obj != null && int.TryParse(obj.ToString(), out uid) && uid > 0)
                        {
                            int result = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteNonQueryBySql("INSERT INTO [Activities_Member]([ActivitiesId] ,[AccountId] ,[MType]) values(" + actId + "," + uid + ",1)");
                            //obj = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteScalarBySql( "INSERT INTO [2014TheSHLearningSite_migration].[dbo].[Activities_Apply]([ActivitiesId] ,[AccountId],[Status],);[Approver] ,[ApproveTime] ,[Mobile],[Email],[RegionId],[StreetId]) values ("++","++",1,0,getdate(),'95279527','testEmail@test.com',4)";
                            isFinsh = true;
                        }
                    }
                }


          }
            //CE0BFD15059B68D67688884D7A3D3E8C
        }
        /// <summary>
        /// 生成激活码。
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="category"></param>
        /// <param name="batch"></param>
        /// <param name="expireDate"></param>
        public void GenActCode(int amount,int category,string batch,DateTime expireDate)
        {
            if (string.IsNullOrEmpty(batch))
            {
                batch = DateTime.Now.ToString("yyyyMMdd");
            }
            string a = "";
            for (int i = 0; i < amount; i++)
            {
                a += "INSERT INTO [ActivationCode_Detail]([Category],[Status],[LotNo],EXPDate)VALUES("+category.ToString ()+",0,'"+batch+"','"+expireDate.ToString()+"');";
            }

            Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteNonQueryBySql(a);

            System.Data.IDataReader dr = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteDataReaderBySql("select id from ActivationCode_Detail where lotno='"+batch+"'");
            List<int> result = new List<int>();
            while (dr.Read())
            {
                result.Add(dr.GetInt32(0));
            }
            dr.Close();
            foreach (int temp in result)
            {
                string tempcode = temp.ToString() + Dianda.Common.StringHelper.GetRandomChar(8 - temp.ToString().Length, 2) + Dianda.Common.StringHelper.GetRandomChar(4, 3);
               
                Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteNonQueryBySql("update ActivationCode_Detail set ActCode='" + Dianda.Common.StringSecurity.AES128.Encrypt(tempcode, "www.shlll.net/ac") + "',ActCodeOrigin='" + tempcode + "' where id=" + temp.ToString());
            }
        }
        //为现有的环保袋幼儿园生成激活码（已经有的则不生成）
        public string GenUploadCode4Kindergarten()
        {
            List<int> Kindergartens = new List<int>();
            System.Data.IDataReader dr = Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteDataReaderBySql("select id from TempActivity_ReusableBagsKindergarten where id not in (select Kindergarten from TempActivity_ReusableBagsUploadCode where actid=138 and canuse=1 and delflag=0 and viptype=0 and kindergarten is not null) and delflag=0 ");
            while (dr.Read())
            {
                Kindergartens.Add(dr.GetInt32(0));
            }
            dr.Close();
            string sql="";
            foreach (int temp in Kindergartens)
            {
                sql += "INSERT INTO [TempActivity_ReusableBagsUploadCode] ([UploadCode] ,[UploadCnt] ,[LimitCnt],[Kindergarten] ,[ActId] ,[CanUse] ,[VipType])  VALUES ('138"+temp.ToString () + Dianda.Common.StringHelper.GetRandomChar(2, 3) + "',0,10," + temp.ToString() + ",138,1,0);" + Environment.NewLine;
                System.Threading.Thread.Sleep(100);
                //Dianda.DALSqlServer.MSEntLibSqlHelper.ExecuteNonQueryBySql("INSERT INTO [TempActivity_ReusableBagsUploadCode] ([UploadCode] ,[UploadCnt] ,[LimitCnt],[Kindergarten] ,[ActId] ,[CanUse] ,[VipType])  VALUES ('138"+Dianda.Common.StringHelper.GetRandomChar(5,3)+"',0,10,"+temp.ToString ()+",138,1,0);");
            }
            return sql;
        }
    }

    
}