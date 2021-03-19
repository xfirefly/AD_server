using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gdu.ExtendedPaint
{
    public  class CursorsHolder
    {
        private  Cursor _pan;
        private  Cursor _line;
        private  Cursor _rect;
        private  Cursor _ellipse;
        private  Cursor _rotate;
        private  Cursor _size_0;
        private  Cursor _size_90;
        private  Cursor _select;
        private  Cursor _select2;

        private static volatile CursorsHolder instance;
        private static object syncRoot = new Object();
 
        public static CursorsHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CursorsHolder();
                    }
                }

                return instance;
            }
        }

        private CursorsHolder()
        {
            List<Cursor> listCursor = new List<Cursor>();
            List<byte[]> listByte = new List<byte[]>();

            listByte.Add(Properties.Resources.pan);
            listByte.Add(Properties.Resources.tool_line);
            listByte.Add(Properties.Resources.tool_rect);
            listByte.Add(Properties.Resources.tool_ellipse);
            listByte.Add(Properties.Resources.rotate); //
            listByte.Add(Properties.Resources.rotate); //size_0);
            listByte.Add(Properties.Resources.rotate); //size_90);
            listByte.Add(Properties.Resources.select);
            listByte.Add(Properties.Resources.select2);

            for (int i = 0; i < listByte.Count; i++)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(listByte[i]))
                {
                    listCursor.Add(new Cursor(ms));
                }
            }

            _pan = listCursor[0];
            _line = listCursor[1];
            _rect = listCursor[2];
            _ellipse = listCursor[3];
            _rotate = listCursor[4];
            _size_0 = listCursor[5];
            _size_90 = listCursor[6];
            _select = listCursor[7];
            _select2 = listCursor[8];

            //_pan = new Cursor(typeof(Cursor), "pan");
            //_line = new Cursor(typeof(Cursor), "tool_line");
            //_rect = new Cursor(typeof(Cursor), "tool_rect");
            //_ellipse = new Cursor(typeof(Cursor), "tool_ellipse");
            //_rotate = new Cursor(typeof(Cursor), "rotate");
            //_size_0 = new Cursor(typeof(Cursor), "size_0");
            //_size_90 = new Cursor(typeof(Cursor), "size_90");
            //_select = new Cursor(typeof(Cursor), "select");
            //_select2 = new Cursor(typeof(Cursor), "select2");
        }

        public  Cursor Pan { get { return _pan; } }
        public  Cursor ToolLine { get { return _line; } }
        public  Cursor ToolRect { get { return _rect; } }
        public  Cursor ToolEllipse { get { return _ellipse; } }
        //publiic Cursor Rotate { get { return _rotate; } }
        public  Cursor Size0 { get { return _size_0; } }
        public  Cursor Size90 { get { return _size_90; } }
        public  Cursor Select { get { return _select; } }
        public  Cursor Select2 { get { return _select2; } }
    }
}
