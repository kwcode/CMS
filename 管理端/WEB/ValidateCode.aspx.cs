using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class ValidateCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string randomcode = string.Empty;
            int ValidateCodeLength = UICommon.Util.ConvertToInt32(Session["ValidateCodeLength"]);
            if (ValidateCodeLength <= 4)
            {
                randomcode = CreateRandNumber(4);
                Session["ValidateCode"] = randomcode;
            }
            else if (ValidateCodeLength > 4 && ValidateCodeLength <= 8)
            {
                ValidateCodeLength = 2;
                randomcode = CreateRegionCode(ValidateCodeLength);
                Session["ValidateCode"] = randomcode;
            }
            else
            {
                ValidateCodeLength = 4;
                randomcode = CreateRegionCode(ValidateCodeLength);
                Session["ValidateCode"] = randomcode;
            }
            //调用函数产生2个随机中文汉字编码 

            //ViewState["ValidateCode"] = randomcode;
            this.CreateImage(randomcode);
        }
        /**/
        /// <summary>
        /// 中文
        /// </summary>
        /// <param name="strlength"></param>
        /// <returns></returns>
        public string CreateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素
            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] rBase = Vchar.Split(',');//拆分成数组

            //string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            Random rnd = new Random();

            //定义一个object数组用来 
            object[] bytes = new object[strlength];

            /**/
            /**/
            /**/
            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中 
            每个汉字有四个区位码组成 
            区位码第1位和区位码第2位作为字节数组第一个元素 
            区位码第3位和区位码第4位作为字节数组第二个元素 
            */
            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位 
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位 
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的种子避免产生重复值 
                int r2;
                if (r1 == 13)
                    r2 = rnd.Next(0, 7);
                else
                    r2 = rnd.Next(0, 16);
                string str_r2 = rBase[r2].Trim();

                //区位码第3位 
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位 
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                    r4 = rnd.Next(1, 16);
                else if (r3 == 15)
                    r4 = rnd.Next(0, 15);
                else
                    r4 = rnd.Next(0, 16);
                string str_r4 = rBase[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中 
                bytes.SetValue(str_r, i);

            }

            //获取GB2312编码页（表） 
            Encoding gb = Encoding.GetEncoding("gb2312");

            //根据汉字编码的字节数组解码出中文汉字 
            string strResult = "";
            for (int i = 0; i < strlength; i++)
            {
                strResult += gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
            }

            return strResult;
        }

        public string CreateRandNumber(int strlength)
        {
            string strResult = UICommon.Util.GetRandNumber(4);
            return strResult;
        }
        /**/
        ///  <summary>
        ///  创建随机码图片
        ///  </summary>
        ///  <param  name="randomcode">随机码</param>
        private void CreateImage(string randomcode)
        {
            int randAngle = 30; //随机转动角度
            int mapwidth = (int)(randomcode.Length * 35);
            Bitmap map = new Bitmap(mapwidth, 35);//创建图片背景
            Graphics graph = Graphics.FromImage(map);
            graph.Clear(Color.AliceBlue);//清除画面，填充背景
            graph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框
            //graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//模式

            Random rand = new Random();
            //背景噪点生成
            Pen blackPen = new Pen(Color.LightGray, 0);
            for (int i = 0; i < 50; i++)
            {
                int x = rand.Next(0, map.Width);
                int y = rand.Next(0, map.Height);
                graph.DrawRectangle(blackPen, x, y, 1, 1);
            }

            //验证码旋转，防止机器识别
            char[] chars = randomcode.ToCharArray();//拆散字符串成单字符数组
            //文字距中
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //定义字体 
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            for (int i = 0; i < chars.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);
                Font f = new System.Drawing.Font(font[findex], 13, System.Drawing.FontStyle.Bold);//字体样式(参数2为字体大小)
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                Point dot = new Point(22, 16);
                //graph.DrawString(dot.X.ToString(),fontstyle,new SolidBrush(Color.Black),10,150);//测试X坐标显示间距的
                float angle = rand.Next(-randAngle, randAngle);//转动的度数
                graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                graph.RotateTransform(angle);
                graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
                //graph.DrawString(chars[i].ToString(),fontstyle,new SolidBrush(Color.Blue),1,1,format);
                graph.RotateTransform(-angle);//转回去
                graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置
            }
            //graph.DrawString(randomcode,fontstyle,new SolidBrush(Color.Blue),2,2); //标准随机码

            //生成图片
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            map.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/gif";
            Response.BinaryWrite(ms.ToArray());
            graph.Dispose();
            map.Dispose();
        }
    }
}