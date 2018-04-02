using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace VKR_FlangeCoupling
{
    class Stupica
    {
        /// <summary>
        /// Крутящий момент
        /// </summary>
        public double m_kr;

        /// <summary>
        /// Диаметр вала
        /// </summary>
        public double d;

        /// <summary>
        /// Внешний диамтер стакана
        /// </summary>
        public double d1;

        /// <summary>
        /// Размер фаски ребер
        /// </summary>
        public double b1;

        /// <summary>
        /// Размер фаски отверстий
        /// </summary>
        public double b2;

        /// <summary>
        /// Габаритная длина муфты
        /// </summary>
        public double l;

        /// <summary>
        /// Высота/толщина фланца
        /// </summary>
        public double l1;

        /// <summary>
        /// Высота выреза l2
        /// </summary>
        public double l2;

        /// <summary>
        /// Диаметр выреза d2
        /// </summary>
        public double d2;

        /// <summary>
        /// Габаритный диаметр муфты
        /// </summary>
        public double D_external;

        /// <summary>
        /// Диаметр окружности крепления болтов
        /// </summary>
        public double D_okr;

        /// <summary>
        /// Ширина паза
        /// </summary>
        public double B;

        /// <summary>
        /// Диаметр+паз - h=d+t2 
        /// </summary>
        public double h;

        /// <summary>
        /// Радиус скругления паза
        /// </summary>
        public double R;

        /// <summary>
        /// Радиус скругления у фланца и стакана
        /// </summary>
        public double R1;

        /// <summary>
        /// Количество отверстий для установки с зазором
        /// </summary>
        public int n;

        /// <summary>
        /// Диаметр отверстий для установки с зазором
        /// </summary>
        public double d3;

        /// <summary>
        /// Отверстия d3. 0 - включено в расчет, 1 - исключено из расчета
        /// </summary>
        public double n_disabled;

        /// <summary>
        /// Угол смещения отверстия d3
        /// </summary>
        public double n_start_angle;

        /// <summary>
        /// Шаг смещения отверстия d3
        /// </summary>
        public double n_step;

        /// <summary>
        /// Направление расстановки отверстий d3. 0 - прямое, 1 - обратное
        /// </summary>
        public double n_direction;

        /// <summary>
        /// Количество отверстий для установки без зазора
        /// </summary>
        public int n3;

        /// <summary>
        /// Диаметр отверстий для установки без зазора
        /// </summary>
        public double d4;

        /// <summary>
        /// Отверстия d4. 0 - включено в расчет, 1 - исключено из расчета
        /// </summary>
        public double n3_disabled;

        /// <summary>
        /// Угол смещения отверстия d4
        /// </summary>
        public double n3_start_angle;

        /// <summary>
        /// Шаг смещения отверстия d4
        /// </summary>
        public double n3_step;

        /// <summary>
        /// Направление расстановки отверстий d4. 0 - прямое, 1 - обратное
        /// </summary>
        public double n3_direction;

        /// <summary>
        /// Максимальная масса муты по ГОСТ
        /// </summary>
        public double m_max;

        GOST_Provider provider = null;

        public Stupica()
        {
            try
            {
                provider = new GOST_Provider();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        double my_celling(double x, double step)
        {
            return Math.Ceiling(x * (1 / step)) / (1 / step);
        }


        public void Get_from_GOST(double Diam, double T, int ispolnenie)
        {
            string parameters = $"m_kr = {T}, d_inner = {Diam}, isp = {ispolnenie}";

            DataTable dt = provider.get_rows("20761-96", "a1", "*", parameters);

            List<string> listGOST10948 = provider.get_one_column_as_List("10948-64", "1", "r", "");
            List<double> doublelistGOST10948 = listGOST10948.Select(double.Parse).ToList();

            m_kr = double.Parse(dt.Rows[0]["m_kr"].ToString());
            d = double.Parse(dt.Rows[0]["d_inner"].ToString());
            h = double.Parse(dt.Rows[0]["h"].ToString());
            B = double.Parse(dt.Rows[0]["b"].ToString());
            b1 = double.Parse(dt.Rows[0]["b1"].ToString());
            b2 = double.Parse(dt.Rows[0]["b2"].ToString());
            D_external = double.Parse(dt.Rows[0]["d_external"].ToString());
            D_okr = double.Parse(dt.Rows[0]["d_okr"].ToString());
            d1 = double.Parse(dt.Rows[0]["d1"].ToString());
            d2 = double.Parse(dt.Rows[0]["d2"].ToString());
            d3 = double.Parse(dt.Rows[0]["d3"].ToString());
            d4 = double.Parse(dt.Rows[0]["d4"].ToString());
            n = int.Parse(dt.Rows[0]["n"].ToString());
            n3 = int.Parse(dt.Rows[0]["n3"].ToString());
            l = double.Parse(dt.Rows[0]["l"].ToString());
            l1 = double.Parse(dt.Rows[0]["l1"].ToString());
            l2 = double.Parse(dt.Rows[0]["l2"].ToString());
            R = double.Parse(dt.Rows[0]["r"].ToString());
            m_max = double.Parse(dt.Rows[0]["m_max"].ToString());

            R1 = (D_external - d1) / 2 * 0.2;
            R1 = my_celling(R1, 0.5);
            R1 = find_nearest_value(doublelistGOST10948, d1 * 0.05);
            // выбирается ближайшее значение по ГОСТ 10948

        }

        /// <summary>
        /// Расчет параметров ступицы
        /// </summary>
        public void Calculate_Param_of_Stupica(double Diam, double T, double l_stupici, int ispolnenie)
        {

            // d = 28;
            d = Diam;

            d1 = d * 1.5;
            d1 = my_celling(d1, 5); //округляем до ближайшего большего целого числа с шагом 5

            //для конкретного исполнения выбираем l из гост 12080, или ставим вручную, если ispolnenie = 3
            l = l_stupici;

            /////////// для исполнения 1 находим по табл.2 ГОСТ 23360 размер t2, считаем h
            /////////// для исполнения 2, d<30  находим по табл.2 ГОСТ 23360 
            /////////// для исполнения 2, d>=30 находим по табл.2 ГОСТ 10748

            DataTable dt23360 = null;

            if (ispolnenie == 2 && d >= 30)
            {
                dt23360 = provider.get_rows("10748-79", "2", "d_ot, d_do, b, t2, r1_ot, r1_do", $"d_ot < {d}, {d} <= d_do");
            }
            else //if (ispolnenie == 1 || (ispolnenie == 2 && d < 30))
            {
                dt23360 = provider.get_rows("23360-78", "2", "d_ot, d_do, b, t2, r1_ot, r1_do", $"d_ot < {d}, {d} <= d_do");
            }

            double t2 = double.Parse(dt23360.Rows[0]["t2"].ToString());
            h = d + t2;
            B = double.Parse(dt23360.Rows[0]["b"].ToString());


            /////////// также берем диапазон размеров фаски и выбираем среднее значение
            double temp_r1 = double.Parse(dt23360.Rows[0]["r1_ot"].ToString());
            double temp_r2 = double.Parse(dt23360.Rows[0]["r1_do"].ToString());
            R = (temp_r1 + temp_r2) / 2;
            R = my_celling(R, 0.1);
            if (R < temp_r1 || R > temp_r2) { R = temp_r2; }


            List<string> listGOST10948 = provider.get_one_column_as_List("10948-64", "1", "r", "");
            List<double> doublelistGOST10948 = listGOST10948.Select(double.Parse).ToList();

            // R1 = (D_external - d1) / 2 * 0.15;
            R1 = d1 * 0.05;
            R1 = my_celling(R1, 0.5);
            R1 = find_nearest_value(doublelistGOST10948, R1);
            // выбирается ближайшее значение по ГОСТ 10948


            int z;
            int m;
            Calculate_Bolts(d1, T, out z, out D_okr, out m, 400, R1);
            //z - количество болтов. ставятся через 1
            //
            n = (int)Math.Floor((double)z / 2);

            n3 = z - this.n;
            // выбрать d3 по ГОСТ 11284, исходя из размера болта
            // d4 выбирается по d1 болта по ГОСТ 7817 -- можно просто +1 к d
            DataTable dt11284 = provider.get_rows("11284-75", "1", "d, d1_1", $"d = {m}");
            d3 = double.Parse(dt11284.Rows[0]["d1_1"].ToString());
            d4 = m + 1;

            D_external = D_okr + 2 * d4;

            l1 = Math.Ceiling(0.12 * D_external);
            
            d2 = d1;
            d2 = my_celling(d2, 5); //округляем до ближайшего большего целого числа с шагом 5


            if (l1 <= 20)
            { l2 = 2; }
            else
            { l2 = 5; }


            b1 = 0.1 * Math.Sqrt(D_external);
            b1 = find_nearest_value(doublelistGOST10948, b1);
            //b1 = выбирается ближайшее значение по ГОСТ 10948

            b2 = 0.25 * Math.Sqrt(d4);
            b2 = find_nearest_value(doublelistGOST10948, b2);
            //b2 = выбирается ближайшее значение по ГОСТ 10948



        }

       public double find_nearest_value(List<double> list, double value)
        {
            double result = 0;
            for (int i = 0; i < list.Count; i++)
            {
                var r = value - list[i];
                if (r <= 0 && i > 0) //если при переборе число из листа стало больше чем наше число - остановимся
                {

                    if (Math.Abs(list[i] - value) > Math.Abs(list[i - 1] - value))  //если разница с большим числом больше, то выбираем меньшее число
                    {
                        result = list[i - 1];
                        break;
                    }
                    else
                    {
                        result = list[i];
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Рассчет кол-ва и размера болтов
        /// </summary>
        void Calculate_Bolts(double d1, double T, out int z, out double D1, out int m, double t_mat = 400, double R1 = 0)
        {

            double t_srez = 0.2 * t_mat;
            double t = double.MaxValue;

            z = 2;  //кол-во болтов
            D1 = 0;  //диаметр на котром расположены болты
            m = 0;  //размер болта


            do
            {

                m = 8;

                do
                {

                    D1 = d1 + R1 + 4 * (m + 1);
                    // по правилам отступается 2d от края, +1 добавляется, т.к. без зазора болт устанавливается в отв. расточенное на +1

                    double Ft = (2 * T * Math.Pow(10, 3)) / (z * D1);
                    t = (4 * Ft) / (Math.PI * Math.Pow(m, 2));
                    //если получилось напряжение на срез меньше чем допустимое, либо перебрали все болты, то берем на 1-2 болта больше

                    if (m >= 20 && t < t_srez * 1.05)
                    {
                        D1 += 5;
                        Ft = (2 * T * Math.Pow(10, 3)) / (z * D1);
                        t = (4 * Ft) / (Math.PI * Math.Pow(m, 2));
                    }

                    if (t > t_srez)
                    {
                        Pick_next_bolt(ref m);
                    }

                } while (m != 0 && t > t_srez);

                if (t > t_srez)
                {
                    if (z < 6)
                    {
                        z++;
                    }
                    else
                    {
                        z += 2;
                    }
                }

            } while (t > t_srez);


        }


        /// <summary>
        /// Расчет расположения отверстий || type - master/slave = 1/2
        /// </summary>
        public void Calculate_Params_of_Holes(int type)  //type - master/slave = 1/2
        {

            if (this.n <= 1) //если 1 отверсие d3, то исключаем из расчета массив d3
            {
                this.n_disabled = 1;
            }
            else
            {
                this.n_disabled = 0;
            }

            if (this.n3 <= 1) //если 1 отверсие d4, то исключаем из расчета массив d4
            {
                this.n3_disabled = 1;
            }
            else
            {
                this.n3_disabled = 0;
            }

            int z = this.n + this.n3;

            this.n_start_angle = 0;
            this.n3_start_angle = 180;
            this.n_step = 0;
            this.n3_step = 0;
            if (z == 3)
            {
                this.n3_start_angle = 120;
                this.n3_step = 120;
            }
            else if (z == 5)
            {
                this.n_start_angle = 0;
                this.n_step = 144;
                this.n3_start_angle = -72;
                this.n3_step = 144;
            }
            else if (z % 2 == 0)
            {
                this.n_start_angle = 0;
                this.n_step = 360 / z * 2;
                this.n3_start_angle = 360 / z;
                this.n3_step = 360 / z * 2;
            }

            if (type == 1)
            {
                this.n_direction = 0;
                this.n3_direction = 0;
            }
            else if (type == 2)
            {
                this.n_direction = 1;
                this.n3_direction = 1;
                this.n_start_angle = -this.n_start_angle;
                this.n3_start_angle = -this.n3_start_angle;
            }
        }

        void Pick_next_bolt(ref int m)
        {
            var ss = provider.get_one_column_as_List("7796-70", "1", "m");

            var m1 = m;

            var mm = ss.FirstOrDefault(o => int.Parse(o.ToString()) > m1);

            if (mm == null)
            {
                mm = "0";
            }

            m = int.Parse(mm);
        }


        double Calculate_Bolt_diam(double t_mat, double D1, int z, double T)
        {
            double t_srez = 0.2 * t_mat;
            return Math.Sqrt((8 * T * Math.Pow(10, 3)) / (t_srez * Math.PI * D1 * z));
        }


    }
}
