using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKR_FlangeCoupling.Details
{
    class Bolt
    {
        /// <summary>
        /// Номинальный диаметр резьбы
        /// </summary>
        public double d;


        /// <summary>
        /// Радиус скругления под головкой
        /// </summary>
        public double R;

        /// <summary>
        /// Диаметр стержня
        /// </summary>
        public double d1;
        
        /// <summary>
        /// Длина болта от головки
        /// </summary>
        public double l;
        
        /// <summary>
        /// Длина резьбы
        /// </summary>
        public double b;

        /// <summary>
        /// Высота головки
        /// </summary>
        public double k;

        /// <summary>
        /// Размер "под ключ"
        /// </summary>
        public double S;

        /// <summary>
        /// Шаг резьбы
        /// </summary>
        public double t;

        /// <summary>
        /// Размер фаски резьбы
        /// </summary>
        public double z;

    }
}
