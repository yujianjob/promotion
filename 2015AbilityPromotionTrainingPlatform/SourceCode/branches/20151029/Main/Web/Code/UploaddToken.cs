using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.Model
{
    public class UploaddToken
    {
        /// <summary>
        /// 必填
        /// 指定上传的目标资源空间（ Bucket ）和资源名（Key），有两种格式：
        /// <bucket> 表示允许用户上传文件到指定的 表示允许用户上传文件到指定的 bucket。
        /// <bucket>:<filename>表示允许用户上传指定 filename 的文件。
        /// 当 overwrite为 1时，已存在同名资源则会被覆盖。如果希望只能上传指定 时，已存在同名资源则会被覆盖。如果希望只能上传指定 时，已存在同名资源则会被覆盖。如果希望只能上传指定 filename 的文件，并且不允许修改那么可以将 overwrite属性值设为 0。
        /// </summary>
        public string scope { get; set; }

        /// <summary>
        /// 必填
        /// 上传请求授权的截止时间；UNIX时间戳，单位：豪秒。范例：1398916800000，代表时间2014-05-01 12:00:00。
        /// </summary>
        public string deadline { get; set; }

        /// <summary>
        /// Web端文件上传成功或失败后，浏览器都会执行303跳转的URL，通常用于 HTML Form 上传。
        ///  文件上传成功后会跳转到<returnUrl>?upload<returnUrl>?upload_ret=<queryString>
        /// 其中 <queryString>包含returnBody内容。
        ///  文件上传失败后会跳转到<returnUrl>?c<returnUrl>?code=<code>&message=<message>
        /// 其中<code>是错误码，<message>是错误具体信息。
        /// 如不设置returnUrl，则直接将returnBody的内容返回给客户端。
        /// </summary>
        public string returnUrl { get; set; }

        /// <summary>
        /// 上传成功后，自定义最终返回給上传端（该字段配合returnUrl使用）的数据。
        /// 如果您只需要返回文件大小和文件地址，只需将returnBody设置成fname=$(fname)&fsize=$(fsize)&url=$(url)即可
        /// </summary>
        public string returnBody { get; set; }

        /// <summary>
        /// 指定是否覆盖服务器上已经存在的文件；0:不覆盖；1：覆盖。
        /// </summary>
        public int? overwrite { get; set; }

        /// <summary>
        /// 限定上传文件的大小，单位：字节（Byte）；超过限制的上传内容会被判为上传失败，返回413状态码。
        /// </summary>
        public long? fsizeLimit { get; set; }

        public string callbackUrl { get; set; }
        public string callbackBody { get; set; }
        public string persistentOps { get; set; }
        public string persistentNotifyUrl { get; set; }

    }
}
