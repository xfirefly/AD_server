using Common;
using ControlManager;
using Ctrl;
using Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextSender
{

    public class Box
    {
        public string ip;
        public string name;
        public string id;

        public CommandChannel CmdChan;
 

        public override string ToString()
        {
            //string txt = string.Format("{0} [{1}] [{2}]", name, id, ip);
            return string.Format("{0} [{1}]", name, ip);
        }
    }
}
