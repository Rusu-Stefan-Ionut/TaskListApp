using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_2_Notite.Model
{
    public class ImageStorage
    {
        private static List<string> catJpg;
        private static int index;
        public List<string> CatJpg
        {
            get { return catJpg; }
            set { catJpg = value; }
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public ImageStorage()
        {
            index = 0;
            catJpg = new List<string>();
            catJpg.Add("../Resources/images/tree.png");
            catJpg.Add("../Resources/images/Coco.jpg");
            catJpg.Add("../Resources/images/Fanica.jpg");
            catJpg.Add("../Resources/images/CosticaFulger.jpg");
            catJpg.Add("../Resources/images/GicuLicuricu.jpg");
            catJpg.Add("../Resources/images/IonutDiamantu.jpg");
            catJpg.Add("../Resources/images/4doresmorehoes.jpg");
            catJpg.Add("../Resources/images/Gigel.jpg");
        }
    }
}
