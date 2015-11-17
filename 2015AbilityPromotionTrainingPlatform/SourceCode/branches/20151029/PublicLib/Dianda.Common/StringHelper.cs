using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Dianda.CacheFactory;
using System.IO;
using System.Web;
namespace Dianda.Common
{
    public static class StringHelper
    {

        static Dictionary<string, string> dictCharacter;
        public static string KEY
        {
            get
            {
                return "12345678";
            }
        }
        static StringHelper()
        {
            dictCharacter = new Dictionary<string, string>();
            //dictCharacter.Add("<", "&lt");
            //dictCharacter.Add(">", "&gt;");
            //dictCharacter.Add("&", "&amp;");
            //dictCharacter.Add("\"", "&quot;");
            dictCharacter.Add("'", "''");
            dictCharacter.Add("[", "[[]");
            myCache = System.Web.HttpContext.Current.Cache;

        }
        /// <summary>
        /// 将html字符进行转义。
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string SafeString(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                foreach (string key in dictCharacter.Keys)
                {
                    val = val.Replace(key, dictCharacter[key]);

                }
            }
            return val;
        }
        static System.Web.Caching.Cache myCache;
        static TrieFilter getIllegalWords()
        {

            object obj = myCache.Get("IllegalWords");
            if (obj == null)
            {
                obj = CacheHelper.Instance.Get(CacheCatalog.GetOther("IllegalWords"));
                TrieFilter tf = new TrieFilter();
                if (obj != null)
                {
                    foreach (string temp in (obj as List<string>))
                    {
                        tf.AddKey(temp);
                    }

                    myCache.Add("IllegalWords", tf, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 10, 0), System.Web.Caching.CacheItemPriority.Normal, null);


                    return tf;
                }
                else
                {

                    List<string> result = new List<string>();
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/File/") + "tools/IllegalWords.txt", Encoding.UTF8))
                    {
                        string temp = string.Empty;
                        while (sr.Peek() > 0)
                        {
                            temp = sr.ReadLine().Trim();
                            if (!string.IsNullOrEmpty(temp))
                            {
                                result.Add(temp);
                                tf.AddKey(temp);
                            }
                        }
                        sr.Close();
                    }

                    CacheHelper.Instance.Set(CacheCatalog.GetOther("IllegalWords"), result);
                    myCache.Add("IllegalWords", tf, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 10, 0), System.Web.Caching.CacheItemPriority.Normal, null);
                    return tf;
                }
            }
            else
                return (TrieFilter)obj;
        }

        /// <summary>
        /// 文本是否合法，是否有敏感词。false表示不合法。有敏感词。
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool IsValid(string text, out string illegalKey)
        {
            TrieFilter tf = getIllegalWords();
            if (tf != null)
            {
                illegalKey = tf.FindOne(text);
                return string.IsNullOrEmpty(illegalKey);
            }
            else
            {
                illegalKey = string.Empty;
                return false;
            }

            //Console.WriteLine("等级{0},文字:{1}",ciming,ci);  
        }

        const string urlRegex = "^(?<!=http://)([\\w-]+\\.)+[\\w-]+(/[\\w-\\./?%=]*)?";
        public static string HttpFormat(string urlAddress)
        {
            Regex reg = new Regex(urlRegex);
            return reg.Replace(urlAddress, "http://$0");
        }

      

        public static string UrlEncrypt(object value)
        {
            return Utils.UrlEncode(Dianda.Common.StringSecurity.DES.Encrypt(value.ToString(), KEY));
        }

        public static string UrlDecrypt(object value)
        {
            return Dianda.Common.StringSecurity.DES.Decrypt(Utils.UrlDecode(value.ToString()), KEY);
        }
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
           RegexOptions.IgnoreCase);
            //删除HTML 
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"–>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!–.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }


        
        /// <summary>
        /// 英文和数字（小写），注意里面没有数字1和0，英文L和O
        /// </summary>
        /// <param name="type">1数字，2英文，3混合</param>
        /// <returns></returns>
        public static   string GetRandomChar(int lenght, int type)
        {
            if (type > 3 || type < 1)
                type = 3;
            System.Threading.Thread.Sleep(5);
            string[] randomCharList = new string[] { "23456789", "abcdefghijkmnpqrstuvwxyz", "23456789abcdefghijkmnpqrstuvwxyz" };
            string result = "";
            Random lvRan = new Random();
            for (int i = 0; i < lenght; i++)
            {
                result += randomCharList[type-1][lvRan.Next(0, randomCharList[type-1].Length)];            
            }
            return result;
        }

    }

    [Serializable]
    public class TrieFilter
    {
        private Char m_key;
        private Dictionary<Char, TrieFilter> m_values;
        //根节点，不包含字符，即m_key='\0';
        public TrieFilter()
        {
            m_values = new Dictionary<Char, TrieFilter>();
        }
        //用于创建子节点
        TrieFilter(Char key)
        {
            m_key = key;
            m_values = new Dictionary<Char, TrieFilter>();
        }
        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="key"></param>
        public void AddKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            TrieFilter node = this;
            foreach (var c in key)
            {
                TrieFilter subnode;
                if (!node.m_values.TryGetValue(c, out subnode))
                {
                    subnode = new TrieFilter(c);
                    node.m_values.Add(c, subnode);
                }
                node = subnode;
            }
            //最后的结点，表示单词结束，用‘\0’表示，指向一个空对象
            node.m_values['\0'] = null;
        }
        /// <summary>
        /// 检查是否包含非法字符
        /// </summary>
        /// <param name="text">输入文本</param>
        /// <returns>找到的第1个非法字符.没有则返回string.Empty</returns>
        public string FindOne(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                TrieFilter node;
                if (m_values.TryGetValue(text[i], out node))
                {
                    for (int j = i + 1; j < text.Length; j++)
                    {
                        if (node.m_values.TryGetValue(text[j], out node))
                        {
                            if (node.m_values.ContainsKey('\0'))
                            {
                                return text.Substring(i, (j + 1 - i));
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return string.Empty;
        }

         
      

    }


    ///// <summary>
    ///// 敏感词过滤 已忽略大小写 全半角 简繁体差异 排除特殊符号干扰 by http://passer.cnblogs.com
    ///// </summary>
    //public static class FilterKeyWords
    //{


    //    private static readonly object LockObj = new object();
    //    private static FilterKeyWordsNode _root;
    //    private const string TraditionalChinese = "皚藹礙愛翺襖奧壩罷擺敗頒辦絆幫綁鎊謗剝飽寶報鮑輩貝鋇狽備憊繃筆畢斃閉邊編貶變辯辮鼈癟瀕濱賓擯餅撥缽鉑駁蔔補參蠶殘慚慘燦蒼艙倉滄廁側冊測層詫攙摻蟬饞讒纏鏟産闡顫場嘗長償腸廠暢鈔車徹塵陳襯撐稱懲誠騁癡遲馳恥齒熾沖蟲寵疇躊籌綢醜櫥廚鋤雛礎儲觸處傳瘡闖創錘純綽辭詞賜聰蔥囪從叢湊竄錯達帶貸擔單鄲撣膽憚誕彈當擋黨蕩檔搗島禱導盜燈鄧敵滌遞締點墊電澱釣調叠諜疊釘頂錠訂東動棟凍鬥犢獨讀賭鍍鍛斷緞兌隊對噸頓鈍奪鵝額訛惡餓兒爾餌貳發罰閥琺礬釩煩範販飯訪紡飛廢費紛墳奮憤糞豐楓鋒風瘋馮縫諷鳳膚輻撫輔賦複負訃婦縛該鈣蓋幹趕稈贛岡剛鋼綱崗臯鎬擱鴿閣鉻個給龔宮鞏貢鈎溝構購夠蠱顧剮關觀館慣貫廣規矽歸龜閨軌詭櫃貴劊輥滾鍋國過駭韓漢閡鶴賀橫轟鴻紅後壺護滬戶嘩華畫劃話懷壞歡環還緩換喚瘓煥渙黃謊揮輝毀賄穢會燴彙諱誨繪葷渾夥獲貨禍擊機積饑譏雞績緝極輯級擠幾薊劑濟計記際繼紀夾莢頰賈鉀價駕殲監堅箋間艱緘繭檢堿鹼揀撿簡儉減薦檻鑒踐賤見鍵艦劍餞漸濺澗漿蔣槳獎講醬膠澆驕嬌攪鉸矯僥腳餃繳絞轎較稭階節莖驚經頸靜鏡徑痙競淨糾廄舊駒舉據鋸懼劇鵑絹傑潔結誡屆緊錦僅謹進晉燼盡勁荊覺決訣絕鈞軍駿開凱顆殼課墾懇摳庫褲誇塊儈寬礦曠況虧巋窺饋潰擴闊蠟臘萊來賴藍欄攔籃闌蘭瀾讕攬覽懶纜爛濫撈勞澇樂鐳壘類淚籬離裏鯉禮麗厲勵礫曆瀝隸倆聯蓮連鐮憐漣簾斂臉鏈戀煉練糧涼兩輛諒療遼鐐獵臨鄰鱗凜賃齡鈴淩靈嶺領餾劉龍聾嚨籠壟攏隴樓婁摟簍蘆盧顱廬爐擄鹵虜魯賂祿錄陸驢呂鋁侶屢縷慮濾綠巒攣孿灤亂掄輪倫侖淪綸論蘿羅邏鑼籮騾駱絡媽瑪碼螞馬罵嗎買麥賣邁脈瞞饅蠻滿謾貓錨鉚貿麽黴沒鎂門悶們錳夢謎彌覓綿緬廟滅憫閩鳴銘謬謀畝鈉納難撓腦惱鬧餒膩攆撚釀鳥聶齧鑷鎳檸獰甯擰濘鈕紐膿濃農瘧諾歐鷗毆嘔漚盤龐賠噴鵬騙飄頻貧蘋憑評潑頗撲鋪樸譜臍齊騎豈啓氣棄訖牽扡釺鉛遷簽謙錢鉗潛淺譴塹槍嗆牆薔強搶鍬橋喬僑翹竅竊欽親輕氫傾頃請慶瓊窮趨區軀驅齲顴權勸卻鵲讓饒擾繞熱韌認紉榮絨軟銳閏潤灑薩鰓賽傘喪騷掃澀殺紗篩曬閃陝贍繕傷賞燒紹賒攝懾設紳審嬸腎滲聲繩勝聖師獅濕詩屍時蝕實識駛勢釋飾視試壽獸樞輸書贖屬術樹豎數帥雙誰稅順說碩爍絲飼聳慫頌訟誦擻蘇訴肅雖綏歲孫損筍縮瑣鎖獺撻擡攤貪癱灘壇譚談歎湯燙濤縧騰謄銻題體屜條貼鐵廳聽烴銅統頭圖塗團頹蛻脫鴕馱駝橢窪襪彎灣頑萬網韋違圍爲濰維葦偉僞緯謂衛溫聞紋穩問甕撾蝸渦窩嗚鎢烏誣無蕪吳塢霧務誤錫犧襲習銑戲細蝦轄峽俠狹廈鍁鮮纖鹹賢銜閑顯險現獻縣餡羨憲線廂鑲鄉詳響項蕭銷曉嘯蠍協挾攜脅諧寫瀉謝鋅釁興洶鏽繡虛噓須許緒續軒懸選癬絢學勳詢尋馴訓訊遜壓鴉鴨啞亞訝閹煙鹽嚴顔閻豔厭硯彥諺驗鴦楊揚瘍陽癢養樣瑤搖堯遙窯謠藥爺頁業葉醫銥頤遺儀彜蟻藝億憶義詣議誼譯異繹蔭陰銀飲櫻嬰鷹應纓瑩螢營熒蠅穎喲擁傭癰踴詠湧優憂郵鈾猶遊誘輿魚漁娛與嶼語籲禦獄譽預馭鴛淵轅園員圓緣遠願約躍鑰嶽粵悅閱雲鄖勻隕運蘊醞暈韻雜災載攢暫贊贓髒鑿棗竈責擇則澤賊贈紮劄軋鍘閘詐齋債氈盞斬輾嶄棧戰綻張漲帳賬脹趙蟄轍鍺這貞針偵診鎮陣掙睜猙幀鄭證織職執紙摯擲幟質鍾終種腫衆謅軸皺晝驟豬諸誅燭矚囑貯鑄築駐專磚轉賺樁莊裝妝壯狀錐贅墜綴諄濁茲資漬蹤綜總縱鄒詛組鑽緻鐘麼為隻兇準啟闆裡靂餘鍊洩";
    //    private const string SimplifiedChinese = "皑蔼碍爱翱袄奥坝罢摆败颁办绊帮绑镑谤剥饱宝报鲍辈贝钡狈备惫绷笔毕毙闭边编贬变辩辫鳖瘪濒滨宾摈饼拨钵铂驳卜补参蚕残惭惨灿苍舱仓沧厕侧册测层诧搀掺蝉馋谗缠铲产阐颤场尝长偿肠厂畅钞车彻尘陈衬撑称惩诚骋痴迟驰耻齿炽冲虫宠畴踌筹绸丑橱厨锄雏础储触处传疮闯创锤纯绰辞词赐聪葱囱从丛凑窜错达带贷担单郸掸胆惮诞弹当挡党荡档捣岛祷导盗灯邓敌涤递缔点垫电淀钓调迭谍叠钉顶锭订东动栋冻斗犊独读赌镀锻断缎兑队对吨顿钝夺鹅额讹恶饿儿尔饵贰发罚阀珐矾钒烦范贩饭访纺飞废费纷坟奋愤粪丰枫锋风疯冯缝讽凤肤辐抚辅赋复负讣妇缚该钙盖干赶秆赣冈刚钢纲岗皋镐搁鸽阁铬个给龚宫巩贡钩沟构购够蛊顾剐关观馆惯贯广规硅归龟闺轨诡柜贵刽辊滚锅国过骇韩汉阂鹤贺横轰鸿红后壶护沪户哗华画划话怀坏欢环还缓换唤痪焕涣黄谎挥辉毁贿秽会烩汇讳诲绘荤浑伙获货祸击机积饥讥鸡绩缉极辑级挤几蓟剂济计记际继纪夹荚颊贾钾价驾歼监坚笺间艰缄茧检碱硷拣捡简俭减荐槛鉴践贱见键舰剑饯渐溅涧浆蒋桨奖讲酱胶浇骄娇搅铰矫侥脚饺缴绞轿较秸阶节茎惊经颈静镜径痉竞净纠厩旧驹举据锯惧剧鹃绢杰洁结诫届紧锦仅谨进晋烬尽劲荆觉决诀绝钧军骏开凯颗壳课垦恳抠库裤夸块侩宽矿旷况亏岿窥馈溃扩阔蜡腊莱来赖蓝栏拦篮阑兰澜谰揽览懒缆烂滥捞劳涝乐镭垒类泪篱离里鲤礼丽厉励砾历沥隶俩联莲连镰怜涟帘敛脸链恋炼练粮凉两辆谅疗辽镣猎临邻鳞凛赁龄铃凌灵岭领馏刘龙聋咙笼垄拢陇楼娄搂篓芦卢颅庐炉掳卤虏鲁赂禄录陆驴吕铝侣屡缕虑滤绿峦挛孪滦乱抡轮伦仑沦纶论萝罗逻锣箩骡骆络妈玛码蚂马骂吗买麦卖迈脉瞒馒蛮满谩猫锚铆贸么霉没镁门闷们锰梦谜弥觅绵缅庙灭悯闽鸣铭谬谋亩钠纳难挠脑恼闹馁腻撵捻酿鸟聂啮镊镍柠狞宁拧泞钮纽脓浓农疟诺欧鸥殴呕沤盘庞赔喷鹏骗飘频贫苹凭评泼颇扑铺朴谱脐齐骑岂启气弃讫牵扦钎铅迁签谦钱钳潜浅谴堑枪呛墙蔷强抢锹桥乔侨翘窍窃钦亲轻氢倾顷请庆琼穷趋区躯驱龋颧权劝却鹊让饶扰绕热韧认纫荣绒软锐闰润洒萨鳃赛伞丧骚扫涩杀纱筛晒闪陕赡缮伤赏烧绍赊摄慑设绅审婶肾渗声绳胜圣师狮湿诗尸时蚀实识驶势释饰视试寿兽枢输书赎属术树竖数帅双谁税顺说硕烁丝饲耸怂颂讼诵擞苏诉肃虽绥岁孙损笋缩琐锁獭挞抬摊贪瘫滩坛谭谈叹汤烫涛绦腾誊锑题体屉条贴铁厅听烃铜统头图涂团颓蜕脱鸵驮驼椭洼袜弯湾顽万网韦违围为潍维苇伟伪纬谓卫温闻纹稳问瓮挝蜗涡窝呜钨乌诬无芜吴坞雾务误锡牺袭习铣戏细虾辖峡侠狭厦锨鲜纤咸贤衔闲显险现献县馅羡宪线厢镶乡详响项萧销晓啸蝎协挟携胁谐写泻谢锌衅兴汹锈绣虚嘘须许绪续轩悬选癣绚学勋询寻驯训讯逊压鸦鸭哑亚讶阉烟盐严颜阎艳厌砚彦谚验鸯杨扬疡阳痒养样瑶摇尧遥窑谣药爷页业叶医铱颐遗仪彝蚁艺亿忆义诣议谊译异绎荫阴银饮樱婴鹰应缨莹萤营荧蝇颖哟拥佣痈踊咏涌优忧邮铀犹游诱舆鱼渔娱与屿语吁御狱誉预驭鸳渊辕园员圆缘远愿约跃钥岳粤悦阅云郧匀陨运蕴酝晕韵杂灾载攒暂赞赃脏凿枣灶责择则泽贼赠扎札轧铡闸诈斋债毡盏斩辗崭栈战绽张涨帐账胀赵蛰辙锗这贞针侦诊镇阵挣睁狰帧郑证织职执纸挚掷帜质钟终种肿众诌轴皱昼骤猪诸诛烛瞩嘱贮铸筑驻专砖转赚桩庄装妆壮状锥赘坠缀谆浊兹资渍踪综总纵邹诅组钻致钟么为只凶准启板里雳余链泄";
    //    private static readonly Dictionary<char, char> TranslationChinese = TraditionalChinese.Select((c, i) => new { c, i }).ToDictionary(p => p.c, p => SimplifiedChinese[p.i]);

    //    /// <summary>
    //    /// 初始化 使用前必须调用一次
    //    /// </summary>
    //    /// <param name='keyWords'>敏感词列表</param>
    //    public static void Init(string[] keyWords)
    //    {
    //        if (_root != null) return;
    //        lock (LockObj)
    //        {
    //            _root = new FilterKeyWordsNode();
    //            var list = keyWords.Select(p => new string(Translation(p))).Distinct().OrderBy(p => p).ThenBy(p => p.Length).ToList();
    //            for (int i = list.Min(p => p.Length); i <= list.Max(p => p.Length); i++)
    //            {
    //                int i1 = i;
    //                var startList = list.Where(p => p.Length >= i1).Select(p => p.Substring(0, i1)).Distinct();
    //                foreach (var startWord in startList)
    //                {
    //                    var tmp = _root;
    //                    for (int j = 0; j < startWord.Length; j++)
    //                    {
    //                        var t = startWord[j];
    //                        if (tmp.Child == null)
    //                            tmp.Child = new Dictionary<char, FilterKeyWordsNode>();

    //                        if (!tmp.Child.ContainsKey(t))
    //                        {
    //                            tmp.Child.Add(t, new FilterKeyWordsNode { IsEnd = list.Contains(startWord.Substring(0, 1 + j)) });
    //                        }

    //                        tmp = tmp.Child[t];
    //                    }
    //                }
    //            }

    //        }
    //    }

    //    /// <summary>
    //    /// 查找含有的关键词
    //    /// </summary>
    //    public static bool Find(string text, out string[] keyWords)
    //    {

    //        keyWords = Find(text).Select(p => text.Substring(p.Key, p.Value)).Distinct().ToArray();
    //        return keyWords.Length > 0;
    //    }

    //    /// <summary>
    //    /// 简单快速替换
    //    /// </summary>
    //    public static string Replace(string text)
    //    {
    //        var dic = Find(text);
    //        var list = text.ToArray();
    //        foreach (var i in dic)
    //        {
    //            for (int j = i.Key; j < i.Key + i.Value; j++)
    //            {
    //                list[j] = '*';
    //            }
    //        }
    //        return new string(list.ToArray());
    //    }

    //    /// <summary>
    //    /// 自定义过滤
    //    /// </summary>
    //    public static string Replace(string text, ReplaceDelegate replaceAction)
    //    {

    //        var dic = Find(text);
    //        var list = text.ToList();
    //        var offset = 0;
    //        foreach (var i in dic)
    //        {

    //            list.RemoveRange(i.Key + offset, i.Value);
    //            var newText = replaceAction(text.Substring(i.Key, i.Value), i.Key, i.Value);
    //            list.InsertRange(i.Key + offset, newText);

    //            offset = offset + newText.Length - i.Value;
    //        }
    //        return new string(list.ToArray());
    //    }


    //    /// <summary>
    //    /// 位置查找
    //    /// </summary>
    //    private static Dictionary<int, int> Find(string src)
    //    {
    //        if (_root == null)
    //            throw new InvalidOperationException('未初始化');

    //        var findResult = new Dictionary<int, int>();
    //        if (string.IsNullOrEmpty(src))
    //            return findResult;


    //        var text = Translation(src);
    //        int start = 0;

    //        while (start < text.Length)
    //        {
    //            int length = 0;
    //            var firstChar = text[start + length];
    //            var node = _root;

    //            //跳过特殊符号
    //            while (IsSkip(firstChar) && (start + length + 1) < text.Length)
    //            {
    //                start++;
    //                firstChar = text[start + length];
    //            }

    //            //不匹配首字符 移动起始位置
    //            while (!node.Child.ContainsKey(firstChar) && start < text.Length - 1)
    //            {
    //                start++;
    //                firstChar = text[start + length];
    //            }

    //            //部分匹配 移动结束位置 直到不匹配
    //            while (node.Child != null && node.Child.ContainsKey(firstChar))
    //            {
    //                node = node.Child[firstChar];
    //                length++;

    //                if((start + length) == text.Length)
    //                    break;

    //                firstChar = text[start + length];

    //                //跳过忽略词
    //                while (IsSkip(firstChar) && !node.IsEnd && (start + length + 1) < text.Length)
    //                {
    //                    length++;
    //                    firstChar = text[start + length];
    //                }


    //            }

    //            //完整匹配 把起始位置移到结束位置
    //            if (node.IsEnd)
    //            {
    //                findResult.Add(start, length);
    //                start += length - 1;
    //            }

    //            start++;

    //        }

    //        return findResult;


    //    }


    //    /// <summary>
    //    /// 字符预处理
    //    /// </summary>
    //    private static char[] Translation(String src)
    //    {
    //        char[] c = src.ToCharArray();
    //        for (int i = 0; i < c.Length; i++)
    //        {
    //            /*全角=>半角*/
    //            if (c[i] > 0xFF00 && c[i] < 0xFF5F)
    //                c[i] = (char)(c[i] - 0xFEE0);

    //            /*大写=>小写*/
    //            if (c[i] > 0x40 && c[i] < 0x5b)
    //                c[i] = (char)(c[i] + 0x20);

    //            /*繁体=>简体*/
    //            if (c[i] > 0x4E00 && c[i] < 0x9FFF)
    //            {
    //                char chinese;
    //                if (TranslationChinese.TryGetValue(c[i], out chinese))
    //                    c[i] = chinese;
    //            }
    //        }
    //        return c;
    //    }



    //    /// <summary>
    //    /// 跳过特殊符号 ASCII范围 排除 数字字母
    //    /// </summary>
    //    private static bool IsSkip(char firstChar)
    //    {
    //        if (firstChar < '0')
    //            return true;
    //        if (firstChar > '9' && firstChar < 'A')
    //            return true;
    //        if (firstChar > 'Z' && firstChar < 'a')
    //            return true;
    //        if (firstChar > 'z' && firstChar < 128)
    //            return true;
    //        return false;
    //    }


    //    /// <summary>
    //    /// 敏感词树
    //    /// </summary>
    //    private class FilterKeyWordsNode
    //    {
    //        public Dictionary<char, FilterKeyWordsNode> Child;
    //        public bool IsEnd;
    //    }

    //    /// <summary>
    //    /// 自定义过滤方法
    //    /// </summary>
    //    /// <param name='text'>找到的字符串</param>
    //    /// <param name='offset'>起始位置</param>
    //    /// <param name='length'>字符串长度</param>
    //    /// <returns>替换后的</returns>
    //    public delegate string ReplaceDelegate(string text, int offset, int length);


    //}
}
