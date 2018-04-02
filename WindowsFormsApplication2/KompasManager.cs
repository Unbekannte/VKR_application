using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using KAPITypes;
using Kompas6API5;
using Kompas6Constants;
using VKR_FlangeCoupling.Details;
using reference = System.Int32;


namespace VKR_FlangeCoupling
{
    class KompasManager
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        private KompasObject _kompas;
        private string _status = "";
        int _hWnd = -1;

        public object get_current_doc()
        {
            object doc = null;

            try
            {
                doc = (ksDocument3D)_kompas.ActiveDocument3D();
            }
            catch (Exception)
            {
                //throw;
            }
            if (doc == null)
            {
                try
                {
                    doc = (ksDocument2D)_kompas.ActiveDocument2D();
                }
                catch (Exception )
                {
                    //throw;
                }
            }


            return doc;
        }

        public string createDoc()
        {
            if (_kompas != null)
            {
                // создать новый документ
                // первый параметр - тип открываемого файла
                //  0 - лист чертежа
                //  1 - фрагмент
                //  2 - текстовый документ
                //  3 - спецификация
                //  4 - 3D-модель
                // второй параметр указывает на необходимость выдачи запроса "Файл изменен. Сохранять?" при закрытии файла
                // третий параметр - указатель на IDispatch, по которому Графие вызывает уведомления об изенении своего состояния
                // ф-ия возвращает HANDLE открытого документа
                ksDocument2D doc = (ksDocument2D)_kompas.Document2D();
                if (doc != null)
                {
                    ksDocumentParam docParam = (ksDocumentParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_DocumentParam);
                    if (docParam != null)
                    {
                        docParam.Init();
                        //docParam.type = (int)DocType.lt_DocSheetStandart;
                        docParam.type = (int)DocType.lt_DocFragment;
                        doc.ksCreateDocument(docParam);
                    }
                }
                return null;
            }
            else
            {
                return "Программа не инициализирована";
            }
        }

        public void LoadKompas()
        {
            const string programmId = "KOMPAS.Application.5";
            var t = Type.GetTypeFromProgID(programmId);
            try
            {
                updateStatus("Ищу запущеный КОМПАС-3D");
                _kompas = (KompasObject)Marshal.GetActiveObject(programmId);
                _kompas.Visible = true;
                _kompas.ActivateControllerAPI();
                _hWnd = _kompas.ksGetHWindow();
                updateStatus("КОМПАС-3D запущен!");
                SetForegroundWindow((IntPtr)_hWnd);
            }
            catch
            {
                try
                {
                    updateStatus("Запускаю КОМПАС-3D");
                    _kompas = (KompasObject)Activator.CreateInstance(t);
                    _kompas.Visible = true;
                    _kompas.ActivateControllerAPI();
                    _hWnd = _kompas.ksGetHWindow();
                    updateStatus("КОМПАС-3D запущен!");
                    SetForegroundWindow((IntPtr)_hWnd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось запустить приложение: \n" + ex.Message);
                }
            }
        }


        public void openFile(string filename)
        {
            //убедимся что КОМПАС-3D запущен
            LoadKompas();
            //открываем файл
            if (_hWnd != -1)
            {
                var doc = (ksDocument3D)_kompas.ActiveDocument3D();
                if (doc != null)
                {
                    doc.close();
                }
                doc = (ksDocument3D)_kompas.Document3D();
                doc.Open(filename.Trim(), false);
            }

        }

        public void closeAll()
        {
            if (_kompas != null)
            {
                _kompas.Quit();
                Marshal.ReleaseComObject(_kompas);
            }
        }

        public string GetInfo()
        {
            string info = "";
            try
            {
                info = $"_kompas = {_kompas.ToString()}";
                info = _status;
            }
            catch (NullReferenceException)
            {
                //info = $"_kompas = null";
            }
            return info;
        }

        public void updateStatus(string st = null)
        {
            if (st != null)
            {
                _status = st;
            }
        }

        void winActivate(string name)
        {
            var prc = Process.GetProcessesByName(name);
            if (prc.Length > 0)
            {
                SetForegroundWindow(prc[0].MainWindowHandle);
            }

        }

        public void getParts()
        {
            ksDocument3D doc = _kompas.ActiveDocument3D();
            var parts = doc.PartCollection(true);
            List<ksPart> partsList = new List<ksPart>();

            try
            {
                var i = -1;
                while (true)
                {
                    ksPart part = doc.GetPart(i);
                    if (part == null) { break; }
                    partsList.Add(part);
                    i++;
                }
            }
            catch (Exception)
            {
            }
        }






        public List<kompasVariable> GetVarsOfAssembly()
        {
            if (_kompas == null)
            {
                LoadKompas();
            }

            ksDocument3D doc = _kompas.ActiveDocument3D();

            if (doc == null)
            {
                MessageBox.Show("Файл сборки не открыт!");
                return null;
            }

            var part = doc.GetPart(-1); //parts.GetByName(partname, true, true);

            var vr = part.VariableCollection;

            var count = vr.GetCount;

            List<kompasVariable> Variables = new List<kompasVariable>();


            for (int i = 0; i < count; i++)
            {
                var VAR = vr.GetByIndex(i);
                var kompasVariable = new kompasVariable();

                kompasVariable.displayName = VAR.displayName;
                kompasVariable.name = VAR.name;
                kompasVariable.note = VAR.note;
                kompasVariable.value = VAR.value.ToString();

                Variables.Add(kompasVariable);

            }
            Variables = Variables.OrderBy(o => o.name[2]).ThenBy(o => o.name[Regex.Match(o.name, ".+(_.{1})").Groups[0].Index + 1]).ToList();
            return Variables;
        }


        public void RebuildModel(DataTable table)
        {
            ksDocument3D doc = _kompas.ActiveDocument3D();

            var part = doc.GetPart(-1);

            var vr = part.VariableCollection;

            var count = vr.GetCount;

            List<kompasVariable> Variables = new List<kompasVariable>();
            //создаем новый список переменных
            for (int i = 0; i < count; i++)
            {
                kompasVariable VAR = new kompasVariable();
                VAR.displayName = table.Rows[i]["displayName"].ToString();
                VAR.name = table.Rows[i]["name"].ToString();
                VAR.note = table.Rows[i]["note"].ToString();
                VAR.value = table.Rows[i]["value"].ToString();
                Variables.Add(VAR);
            }
            

            //каджую новую переменную из списка записываем в модель
            foreach (var VAR in Variables)
            {
                ChangeVariableOfAssembly(VAR.name, VAR.value);
            }
            part.RebuildModel();
        }

        void ChangeVariableOfAssembly(string varName, string value)
        {

            if (varName == "value")
            {
                //
            }

            ksDocument3D doc = _kompas.ActiveDocument3D();

            var parts = doc.PartCollection(true);

            var part = doc.GetPart(-1); //parts.GetByName(partname, true, true);

            var vr = part.VariableCollection;

            var count = vr.GetCount;

            var targetVariable = vr.GetByName(varName, true, true);


            part.BeginEdit();
            targetVariable.value = Convert.ToDouble(value);
            part.Update();
            // part.RebuildModel();
            part.EndEdit(true);
            parts.Refresh();
        }

       public void MakeTechnicalDrawings_STUPICA(Stupica stupica)
        {
            double multiply;    // = 0.4;  //масштаб 1:2.5

           if (stupica.D_external <= 30)
           {
               multiply = 5;
           } else if (stupica.D_external > 30 && stupica.D_external <= 37.5)
            {
                multiply = 4;
            }
            else if (stupica.D_external > 37.5 && stupica.D_external <= 60)
            {
                multiply = 2.5;
            }
            else if (stupica.D_external > 60 && stupica.D_external <= 75)
            {
                multiply = 2;
            }
            else if (stupica.D_external > 75 && stupica.D_external <= 150)
            {
                multiply = 1;
            }
            else if (stupica.D_external > 150 && stupica.D_external <= 300)
            {
                multiply = 0.5;
            }
            else if (stupica.D_external > 300 && stupica.D_external <= 375)
            {
                multiply = 0.4;
            }
            else if (stupica.D_external > 375 && stupica.D_external <= 600)
            {
                multiply = 0.25;
            }
            else if (stupica.D_external > 600 && stupica.D_external <= 750)
            {
                multiply = 0.2;
            }
            else
            {
                multiply = 1;
            }


            ksDynamicArray arr = (ksDynamicArray)_kompas.GetDynamicArray(ldefin2d.POINT_ARR);
            ksDocument2D doc = (ksDocument2D)_kompas.Document2D();
            doc = (ksDocument2D)_kompas.Document2D();
            ksDocumentParam docPar = (ksDocumentParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_DocumentParam);
            ksDocumentParam docPar1 = (ksDocumentParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_DocumentParam);

            if ((docPar != null) & (docPar1 != null))
            {
                // docPar.fileName = @"c:\2.cdw";
                //docPar.comment = "Create document";
                docPar.author = "unbekannte";
                docPar.regime = 0;
                docPar.type = (short)DocType.lt_DocSheetStandart;
                ksSheetPar shPar = (ksSheetPar)docPar.GetLayoutParam();
                if (shPar != null)
                {
                    shPar.shtType = 1;
                    shPar.layoutName = string.Empty;
                    ksStandartSheet stPar = (ksStandartSheet)shPar.GetSheetParam();
                    if (stPar != null)
                    {
                        stPar.format = 3;   //2 - А2, 3 - А3 и тд
                        stPar.multiply = 1; //кратность формата, увеличивает лист в размерах
                        stPar.direct = true;    //горизонтально
                    }
                }
                // Создали документ: лист, формат А3, горизонтально расположенный
                // и с системным штампом 1
                doc.ksCreateDocument(docPar);

                ksStamp stamp = (ksStamp)doc.GetStamp();
                if (stamp != null)
                {
                    if (stamp.ksOpenStamp() == 1)
                    {
                        stamp.ksColumnNumber(1);
                        Set_into_Stamp(doc, "Ступица фланцевой муфты");
                        stamp.ksColumnNumber(2);
                        Set_into_Stamp(doc, "-");
                        stamp.ksColumnNumber(3);
                        Set_into_Stamp(doc, "СТАЛЬ 35 ГОСТ 1050-2013");
                        stamp.ksColumnNumber(6);
                        string tmp = "";
                        if (multiply >= 1)
                        {
                            tmp = multiply.ToString() + ":1";
                        }
                        else
                        {
                            var M = Math.Round((1/multiply), 1);
                            tmp = "1:" + M.ToString();
                        }
                        Set_into_Stamp(doc, tmp);
                        stamp.ksColumnNumber(7);
                        Set_into_Stamp(doc, "1");
                        stamp.ksColumnNumber(8);
                        Set_into_Stamp(doc, "3");
                        stamp.ksColumnNumber(9);
                        Set_into_Stamp(doc, "ТулГУ гр. 222131");



                        stamp.ksCloseStamp();
                    }
                }

                int number = 0;
                ksViewParam parv1 = (ksViewParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_ViewParam);
                if (parv1 != null)
                {
                    number = 1;

                    parv1.Init();
                    parv1.x = 100;
                    parv1.y = 150;
                    parv1.scale_ = multiply;
                    parv1.angle = 0;  //угол поворота вида
                    parv1.color = Color.FromArgb(10, 20, 10).ToArgb();    //цвет вида в активном состоянии
                    parv1.state = ldefin2d.stACTIVE;
                    parv1.name = "Вид слева";
                    // У документа создадим вид с номером 1, масштабом 0.5, под углом 45 гр

                    reference v = doc.ksCreateSheetView(parv1, ref number); //создать вид

                    // Создадим слой с номером 3
                    doc.ksLayer(3);

                    //основа
                    doc.ksLineSeg(0, 0,
                        0, (stupica.D_external / 2 - stupica.b1), 1);   //с середины до верха
                    doc.ksLineSeg(0, (stupica.D_external / 2 - stupica.b1),
                        (stupica.b1), (stupica.D_external / 2), 1);  //фаска
                    doc.ksLineSeg((stupica.b1), (stupica.D_external / 2),
                        (stupica.l1 - stupica.b1), (stupica.D_external / 2), 1); //верхняя линия
                    doc.ksLineSeg((stupica.l1 - stupica.b1), (stupica.D_external / 2),
                        (stupica.l1), (stupica.D_external / 2 - stupica.b1), 1);    //faska
                    doc.ksLineSeg((stupica.l1), (stupica.D_external / 2 - stupica.b1),
                        (stupica.l1), (stupica.d1 / 2 + stupica.R1), 1);   //сверху вниз до скругления
                    doc.ksArcByAngle((stupica.l1 + stupica.R1), (stupica.d1 / 2 + stupica.R1), (stupica.R1),
                        180, 270, 1, 1);  //скругление
                    doc.ksLineSeg((stupica.l1 + stupica.R1), (stupica.d1 / 2),
                      (stupica.l - stupica.b1), (stupica.d1 / 2), 1);    //линия внешней стороны стакана вправо
                    doc.ksLineSeg((stupica.l - stupica.b1), (stupica.d1 / 2),
                        (stupica.l), (stupica.d1 / 2 - stupica.b1), 1); //faska
                    doc.ksLineSeg((stupica.l), (stupica.d1 / 2 - stupica.b1),
                        (stupica.l), 0, 1);

                    ///////зеркально
                    doc.ksLineSeg(0, 0,
                            0, -(stupica.D_external / 2 - stupica.b1), 1);   //с середины до верха
                    doc.ksLineSeg(0, -(stupica.D_external / 2 - stupica.b1),
                        (stupica.b1), -(stupica.D_external / 2), 1);  //фаска
                    doc.ksLineSeg((stupica.b1), -(stupica.D_external / 2),
                        (stupica.l1 - stupica.b1), -(stupica.D_external / 2), 1); //верхняя линия
                    doc.ksLineSeg((stupica.l1 - stupica.b1), -(stupica.D_external / 2),
                        (stupica.l1), -(stupica.D_external / 2 - stupica.b1), 1);    //faska
                    doc.ksLineSeg((stupica.l1), -(stupica.D_external / 2 - stupica.b1),
                        (stupica.l1), -(stupica.d1 / 2 + stupica.R1), 1);   //сверху вниз до скругления
                    doc.ksArcByAngle((stupica.l1 + stupica.R1), -(stupica.d1 / 2 + stupica.R1), (stupica.R1),
                        90, 180, 1, 1);  //скругление
                    doc.ksLineSeg((stupica.l1 + stupica.R1), -(stupica.d1 / 2),
                      (stupica.l - stupica.b1), -(stupica.d1 / 2), 1);    //линия внешней стороны стакана вправо
                    doc.ksLineSeg((stupica.l - stupica.b1), -(stupica.d1 / 2),
                        (stupica.l), -(stupica.d1 / 2 - stupica.b1), 1); //faska
                    doc.ksLineSeg((stupica.l), -(stupica.d1 / 2 - stupica.b1),
                        (stupica.l), 0, 1);


                    //верхнее отверстие под болты
                    doc.ksLineSeg((0), (stupica.D_okr / 2 - stupica.d4 / 2),
                              (0), (stupica.D_okr / 2 + stupica.d4 / 2), 1);  //снизу вверх
                    doc.ksLineSeg((0), (stupica.D_okr / 2 + stupica.d4 / 2),
                                (stupica.l1 - stupica.b2), (stupica.D_okr / 2 + stupica.d4 / 2), 1);    //верхняя планка
                    doc.ksLineSeg((stupica.l1 - stupica.b2), (stupica.D_okr / 2 + stupica.d4 / 2),
                                (stupica.l1 - stupica.b2), (stupica.D_okr / 2 - stupica.d4 / 2), 1);    //сверху вниз
                    doc.ksLineSeg((0), (stupica.D_okr / 2 - stupica.d4 / 2),
                                (stupica.l1 - stupica.b2), (stupica.D_okr / 2 - stupica.d4 / 2), 1);//нижняя планка
                    doc.ksLineSeg((stupica.l1 - stupica.b2), (stupica.D_okr / 2 + stupica.d4 / 2),
                                (stupica.l1), (stupica.D_okr / 2 + stupica.d4 / 2 + stupica.b2), 1); //верхняя фаска
                    doc.ksLineSeg((stupica.l1), (stupica.D_okr / 2 + stupica.d4 / 2 + stupica.b2),
                               (stupica.l1), (stupica.D_okr / 2 - stupica.d4 / 2 - stupica.b2), 1);//сверху вниз
                    doc.ksLineSeg((stupica.l1 - stupica.b2), (stupica.D_okr / 2 - stupica.d4 / 2),
                               (stupica.l1), (stupica.D_okr / 2 - stupica.d4 / 2 - stupica.b2), 1); //нижняя фаска

                    //нижнее отверстие под болты
                    doc.ksLineSeg((0), -(stupica.D_okr / 2 - stupica.d4 / 2),
                              (0), -(stupica.D_okr / 2 + stupica.d4 / 2), 1);  //снизу вверх
                    doc.ksLineSeg((0), -(stupica.D_okr / 2 + stupica.d4 / 2),
                                (stupica.l1 - stupica.b2), -(stupica.D_okr / 2 + stupica.d4 / 2), 1);    //верхняя планка
                    doc.ksLineSeg((stupica.l1 - stupica.b2), -(stupica.D_okr / 2 + stupica.d4 / 2),
                                (stupica.l1 - stupica.b2), -(stupica.D_okr / 2 - stupica.d4 / 2), 1);    //сверху вниз
                    doc.ksLineSeg((0), -(stupica.D_okr / 2 - stupica.d4 / 2),
                                (stupica.l1 - stupica.b2), -(stupica.D_okr / 2 - stupica.d4 / 2), 1);//нижняя планка
                    doc.ksLineSeg((stupica.l1 - stupica.b2), -(stupica.D_okr / 2 + stupica.d4 / 2),
                                (stupica.l1), -(stupica.D_okr / 2 + stupica.d4 / 2 + stupica.b2), 1); //верхняя фаска
                    doc.ksLineSeg((stupica.l1), -(stupica.D_okr / 2 + stupica.d4 / 2 + stupica.b2),
                               (stupica.l1), -(stupica.D_okr / 2 - stupica.d4 / 2 - stupica.b2), 1);//сверху вниз
                    doc.ksLineSeg((stupica.l1 - stupica.b2), -(stupica.D_okr / 2 - stupica.d4 / 2),
                               (stupica.l1), -(stupica.D_okr / 2 - stupica.d4 / 2 - stupica.b2), 1); //нижняя фаска

                    //внутр кольцо верх
                    doc.ksLineSeg((0), (0),
                        (0), (stupica.d2 / 2), 1);
                    doc.ksLineSeg((stupica.l2), (0),
                        (stupica.l2), (stupica.d2 / 2), 1);
                    doc.ksLineSeg((0), (stupica.d2 / 2),
                        (stupica.l2), (stupica.d2 / 2), 1);

                    //внутр кольцо низ
                    doc.ksLineSeg((0), (0),
                        (0), -(stupica.d2 / 2), 1);
                    doc.ksLineSeg((stupica.l2), (0),
                        (stupica.l2), -(stupica.d2 / 2), 1);
                    doc.ksLineSeg((0), -(stupica.d2 / 2),
                        (stupica.l2), -(stupica.d2 / 2), 1);

                    //осн. отверстие верх
                    doc.ksLineSeg((stupica.l2), (stupica.d / 2),
                        (stupica.l - stupica.b1), (stupica.d / 2), 1);   //
                    doc.ksLineSeg((stupica.l2), (stupica.h - stupica.d / 2),
                        (stupica.l), (stupica.h - stupica.d / 2), 1);   //
                    doc.ksLineSeg((stupica.l - stupica.b1), (stupica.d / 2),
                        (stupica.l), (stupica.d / 2 + stupica.b1), 1);   //фаска
                    doc.ksLineSeg((stupica.l - stupica.b1), (stupica.d / 2),
                        (stupica.l - stupica.b1), (0), 1);   //фаска

                    //осн. отверстие низ
                    doc.ksLineSeg((stupica.l2), -(stupica.d / 2),
                        (stupica.l - stupica.b1), -(stupica.d / 2), 1);
                    doc.ksLineSeg((stupica.l - stupica.b1), -(stupica.d / 2),
                        (stupica.l), -(stupica.d / 2 + stupica.b1), 1);   //фаска
                    doc.ksLineSeg((stupica.l - stupica.b1), -(stupica.d / 2),
                        (stupica.l - stupica.b1), (0), 1);   //фаска

                    doc.ksLineSeg((0), (0),
                        (0), (0), 1);   //
                    doc.ksLineSeg((0), (0),
                        (0), (0), 1);   //
                    doc.ksLineSeg((0), (0),
                        (0), (0), 1);   //
                    doc.ksLineSeg((0), (0),
                        (0), (0), 1);   //

                    //освевые
                    doc.ksLineSeg((0) - 5, (0),
                        (stupica.l) + 5, (0), 3);   //
                    doc.ksLineSeg((0) - 5, (stupica.D_okr / 2),
                        (stupica.l1) + 5, (stupica.D_okr / 2), 3);   //
                    doc.ksLineSeg((0) - 5, -(stupica.D_okr / 2),
                        (stupica.l1) + 5, -(stupica.D_okr / 2), 3);   //

                    if (doc.ksHatch(0, 45, 2, 0, 0, 0) == 1)
                    {

                        doc.ksLineSeg(0, stupica.D_okr / 2 + stupica.d4 / 2,
                            0, (stupica.D_external / 2 - stupica.b1), 1);   //от отверстия до верха
                        doc.ksLineSeg(0, (stupica.D_external / 2 - stupica.b1),
                            (stupica.b1), (stupica.D_external / 2), 1);  //фаска						
                        doc.ksLineSeg((stupica.b1), (stupica.D_external / 2),
                            (stupica.l1 - stupica.b1), (stupica.D_external / 2), 1); //верхняя линия
                        doc.ksLineSeg((stupica.l1 - stupica.b1), (stupica.D_external / 2),
                            (stupica.l1), (stupica.D_external / 2 - stupica.b1), 1);    //faska
                        doc.ksLineSeg((stupica.l1), (stupica.D_external / 2 - stupica.b1),
                            (stupica.l1), (stupica.D_okr / 2 + stupica.d4 / 2 + stupica.b2), 1);   //сверху вниз до фаски
                        doc.ksLineSeg((stupica.l1 - stupica.b2), (stupica.D_okr / 2 + stupica.d4 / 2),
                                    (stupica.l1), (stupica.D_okr / 2 + stupica.d4 / 2 + stupica.b2), 1); //верхняя фаска
                        doc.ksLineSeg((0), (stupica.D_okr / 2 + stupica.d4 / 2),
                                    (stupica.l1 - stupica.b2), (stupica.D_okr / 2 + stupica.d4 / 2), 1);    //нижняя линия


                        doc.ksLineSeg((0), (stupica.d2 / 2),
                        (0), (stupica.D_okr / 2 - stupica.d4 / 2), 1);   //от кольца до отверстия
                        doc.ksLineSeg((0), (stupica.D_okr / 2 - stupica.d4 / 2),
                                    (stupica.l1 - stupica.b2), (stupica.D_okr / 2 - stupica.d4 / 2), 1);//нижняя планка								
                        doc.ksLineSeg((stupica.l1 - stupica.b2), (stupica.D_okr / 2 - stupica.d4 / 2),
                                   (stupica.l1), (stupica.D_okr / 2 - stupica.d4 / 2 - stupica.b2), 1); //нижняя фаска
                        doc.ksLineSeg((stupica.l1), (stupica.D_okr / 2 - stupica.d4 / 2 - stupica.b2),
                                   (stupica.l1), (stupica.d1 / 2 + stupica.R1), 1); //нижняя фаска
                        doc.ksArcByAngle((stupica.l1 + stupica.R1), (stupica.d1 / 2 + stupica.R1), (stupica.R1),
                            180, 270, 1, 1);  //скругление
                        doc.ksLineSeg((stupica.l1 + stupica.R1), (stupica.d1 / 2),
                          (stupica.l - stupica.b1), (stupica.d1 / 2), 1);    //линия внешней стороны стакана вправо
                        doc.ksLineSeg((stupica.l - stupica.b1), (stupica.d1 / 2),
                            (stupica.l), (stupica.d1 / 2 - stupica.b1), 1); //faska
                        doc.ksLineSeg((stupica.l), (stupica.d1 / 2 - stupica.b1),
                            (stupica.l), (stupica.h - stupica.d / 2), 1);  //до паза
                        doc.ksLineSeg((stupica.l2), (stupica.h - stupica.d / 2),
                            (stupica.l), (stupica.h - stupica.d / 2), 1);   //паз
                        doc.ksLineSeg((stupica.l2), (stupica.h - stupica.d / 2),
                            (stupica.l2), (stupica.d2 / 2), 1);
                        doc.ksLineSeg((0), (stupica.d2 / 2),
                            (stupica.l2), (stupica.d2 / 2), 1);

                        //зеркально
                        doc.ksLineSeg(0, -(stupica.D_okr / 2 + stupica.d4 / 2),
                            0, -(stupica.D_external / 2 - stupica.b1), 1);   //от отверстия до верха
                        doc.ksLineSeg(0, -(stupica.D_external / 2 - stupica.b1),
                            (stupica.b1), -(stupica.D_external / 2), 1);  //фаска						
                        doc.ksLineSeg((stupica.b1), -(stupica.D_external / 2),
                            (stupica.l1 - stupica.b1), -(stupica.D_external / 2), 1); //верхняя линия
                        doc.ksLineSeg((stupica.l1 - stupica.b1), -(stupica.D_external / 2),
                            (stupica.l1), -(stupica.D_external / 2 - stupica.b1), 1);    //faska
                        doc.ksLineSeg((stupica.l1), -(stupica.D_external / 2 - stupica.b1),
                            (stupica.l1), -(stupica.D_okr / 2 + stupica.d4 / 2 + stupica.b2), 1);   //сверху вниз до фаски
                        doc.ksLineSeg((stupica.l1 - stupica.b2), -(stupica.D_okr / 2 + stupica.d4 / 2),
                                    (stupica.l1), -(stupica.D_okr / 2 + stupica.d4 / 2 + stupica.b2), 1); //верхняя фаска
                        doc.ksLineSeg((0), -(stupica.D_okr / 2 + stupica.d4 / 2),
                                    (stupica.l1 - stupica.b2), -(stupica.D_okr / 2 + stupica.d4 / 2), 1);    //нижняя линия


                        doc.ksLineSeg((0), -(stupica.d2 / 2),
                        (0), -(stupica.D_okr / 2 - stupica.d4 / 2), 1);   //от кольца до отверстия
                        doc.ksLineSeg((0), -(stupica.D_okr / 2 - stupica.d4 / 2),
                                    (stupica.l1 - stupica.b2), -(stupica.D_okr / 2 - stupica.d4 / 2), 1);//нижняя планка								
                        doc.ksLineSeg((stupica.l1 - stupica.b2), -(stupica.D_okr / 2 - stupica.d4 / 2),
                                   (stupica.l1), -(stupica.D_okr / 2 - stupica.d4 / 2 - stupica.b2), 1); //нижняя фаска
                        doc.ksLineSeg((stupica.l1), -(stupica.D_okr / 2 - stupica.d4 / 2 - stupica.b2),
                                   (stupica.l1), -(stupica.d1 / 2 + stupica.R1), 1); //нижняя фаска
                        doc.ksArcByAngle((stupica.l1 + stupica.R1), -(stupica.d1 / 2 + stupica.R1), (stupica.R1),
                        90, 180, 1, 1);  //скругление
                        doc.ksLineSeg((stupica.l1 + stupica.R1), -(stupica.d1 / 2),
                          (stupica.l - stupica.b1), -(stupica.d1 / 2), 1);    //линия внешней стороны стакана вправо
                        doc.ksLineSeg((stupica.l - stupica.b1), -(stupica.d1 / 2),
                            (stupica.l), -(stupica.d1 / 2 - stupica.b1), 1); //faska
                        doc.ksLineSeg((stupica.l), -(stupica.d1 / 2 - stupica.b1),
                            (stupica.l), -(stupica.d / 2), 1);  //до отв
                        doc.ksLineSeg((stupica.l - stupica.b1), -(stupica.d / 2),
                        (stupica.l - stupica.b1), -(stupica.d / 2 + stupica.b1), 1);   //фаска
                        doc.ksLineSeg((stupica.l2), -(stupica.d / 2),
                            (stupica.l), -(stupica.d / 2), 1);   //отв
                        doc.ksLineSeg((stupica.l2), -(stupica.d / 2),
                            (stupica.l2), -(stupica.d2 / 2), 1);
                        doc.ksLineSeg((0), -(stupica.d2 / 2),
                            (stupica.l2), -(stupica.d2 / 2), 1);

                        reference telo = doc.ksEndObj();

                        ksHatchParam par = (ksHatchParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_HatchParam);
                        if (par != null)
                        {
                            par.Init();
                            int t = doc.ksGetObjParam(telo, par, ldefin2d.ALLPARAM);

                            doc.ksDeleteMtr();

                            doc.ksMtr(0, 0, 0, 2, 2);

                            // заменить параметры штриховки
                            par.x = 0.8;

                            doc.ksDeleteMtr();
                        }
                    }


                    //////////   РАЗМЕРЫ
                    //горизонтальные
                    LinRazm(doc,
                        (stupica.b1), -(stupica.d2 / 2),
                        (stupica.b1), (stupica.d2 / 2),
                        1, -15, 0, 1);  //d2
                    LinRazm(doc,
                        (stupica.b1), -(stupica.D_external / 2),
                        (stupica.b1), (stupica.D_external / 2),
                        1, -27, 0, 1);  //D_external
                    LinRazm(doc,
                        (stupica.l - stupica.b1), -(stupica.d1 / 2),
                        (stupica.l - stupica.b1), (stupica.d1 / 2),
                        1, 27, 0, 1);   //d1

                    //вертикальные
                    LinRazm(doc,
                        (0), -(stupica.d2 / 2),
                        (stupica.l2), -(stupica.d2 / 2),
                        2, 0, -15, 0);  //l2
                    LinRazm(doc,
                        (stupica.l1 - stupica.b2), -(stupica.D_okr / 2),
                        (stupica.l1), -(stupica.D_okr / 2),
                        2, 0, -(stupica.D_external / 2 - stupica.D_okr / 2) * multiply - 15, 0, 3);  //b2 - фаска отверстия
                    LinRazm(doc,
                        (0), -(stupica.D_external / 2 - stupica.b1),
                        (stupica.l1), -(stupica.D_external / 2 - stupica.b1),
                        2, 0, -27, 0);  //l1
                    LinRazm(doc,
                        (0), (0),
                        (stupica.l), (0),
                        2, 0, -(stupica.D_external / 2) * multiply - 39, 0); //l
                    LinRazm(doc,
                        (stupica.l - stupica.b1), (0),
                        (stupica.l), (0),
                        2, 0, -(stupica.d1 / 2) * multiply - 15, 0, 3);  //b1 - фаска ребра

                    set_rad_razm(stupica, doc);


                    liniya_s_obrivom(stupica, multiply, doc);

                }





                ksViewParam parv2 = (ksViewParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_ViewParam);
                if (parv2 != null)
                {
                    number = 2;
                    parv2.Init();
                    parv2.x = 300;
                    parv2.y = 150;
                    parv2.scale_ = multiply;   //масштаб вида
                    parv2.angle = 0;  //угол поворота вида
                    parv2.color = Color.FromArgb(10, 20, 10).ToArgb();    //цвет вида в активном состоянии
                    parv2.state = ldefin2d.stACTIVE;
                    parv2.name = "Вид спереди";
                    // У документа создадим вид с номером 1, масштабом 0.5, под углом 45 гр

                    reference v = doc.ksCreateSheetView(parv2, ref number); //создать вид

                    // Создадим слой с номером 5
                    doc.ksLayer(5);

                    //внешняя окр
                    doc.ksCircle(0, 0, (stupica.D_external / 2), 1);
                    doc.ksCircle(0, 0, (stupica.D_external / 2 - stupica.b1), 1);

                    //стакан
                    doc.ksCircle(0, 0, (stupica.d1 / 2), 1);
                    doc.ksCircle(0, 0, (stupica.d1 / 2 - stupica.b1), 1);

                    //осевая
                    doc.ksCircle(0, 0, (stupica.D_okr / 2), 3);

                    //паз
                    reference left = doc.ksLineSeg((-stupica.B / 2), (stupica.d / 2 - stupica.b1 * 5),
                          (-stupica.B / 2), (stupica.h - stupica.d / 2 - stupica.R), 1);   //левая линия
                    reference right = doc.ksLineSeg((stupica.B / 2), (stupica.d / 2 - stupica.b1 * 5),
                         (stupica.B / 2), (stupica.h - stupica.d / 2 - stupica.R), 1);   //правая линия
                    doc.ksArcByAngle((-stupica.B / 2 + stupica.R), (stupica.h - stupica.d / 2 - stupica.R), (stupica.R),
                        90, 180, 1, 1);  //скругление
                    doc.ksArcByAngle((stupica.B / 2 - stupica.R), (stupica.h - stupica.d / 2 - stupica.R), (stupica.R),
                        0, 90, 1, 1);  //скругление
                    doc.ksLineSeg((-stupica.B / 2 + stupica.R), (stupica.h - stupica.d / 2),
                        (stupica.B / 2 - stupica.R), (stupica.h - stupica.d / 2), 1);   //верхняя линия

                    //внутр окр
                    reference c1 = doc.ksCircle(0, 0, (stupica.d / 2), 1);
                    reference c2 = doc.ksCircle(0, 0, (stupica.d / 2 + stupica.b1), 1);

                    /*     doc.ksTrimmCurve(left,
                             (-master.B / 2), (master.h - master.d / 2 - master.R),
                             (-master.B / 2), (master.d / 2),
                             (-master.B / 2), (master.d / 2 + 0.5), 1);*/


                    ksMathPointParam p1 = (ksMathPointParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_MathPointParam);


                    object afa = new object();

                    //точки пересечения считаю сам, т.к. компас... не будем говорить о плохом
                    var c1_l1 = line_cirle_itersection((stupica.d / 2), (-stupica.B / 2));
                    var c1_l2 = line_cirle_itersection((stupica.d / 2), (stupica.B / 2));
                    var c2_l1 = line_cirle_itersection((stupica.d / 2 + stupica.b1), (-stupica.B / 2));
                    var c2_l2 = line_cirle_itersection((stupica.d / 2 + stupica.b1), (stupica.B / 2));

                    doc.ksTrimmCurve(left,
                        (-stupica.B / 2), (stupica.h - stupica.d / 2 - stupica.R),
                        c1_l1.x, c1_l1.y, c1_l1.x, c1_l1.y, 1);
                    doc.ksTrimmCurve(right,
                        (stupica.B / 2), (stupica.h - stupica.d / 2 - stupica.R),
                        c1_l2.x, c1_l2.y, c1_l2.x, c1_l2.y, 1);
                    doc.ksTrimmCurve(c1, c1_l1.x, c1_l1.y, c1_l2.x, c1_l2.y, 0, -(stupica.d / 2), 1);
                    doc.ksTrimmCurve(c2, c2_l1.x, c2_l1.y, c2_l2.x, c2_l2.y, 0, -(stupica.d / 2 + stupica.b1), 1);

                    doc.ksLineSeg((0), (0),
                        (0), (0), 1);   //
                    doc.ksLineSeg((0), (0),
                        (0), (0), 1);   //
                    doc.ksLineSeg((0), (0),
                        (0), (0), 1);   //

                    int N_otv = (stupica.n + stupica.n3);
                    int angle = 360 / N_otv;
                    List<ksMathPointParam> list_of_holes = new List<ksMathPointParam>();
                    for (int i = 1; i <= N_otv; i++)
                    {
                        var c_otv = point_by_angle((stupica.D_okr / 2), 90 + angle * (i - 1));
                        list_of_holes.Add(c_otv);
                        if (i % 2 == 1)
                        {
                            doc.ksCircle(c_otv.x, c_otv.y, (stupica.d4 / 2), 1);
                            doc.ksLineSeg(c_otv.x, c_otv.y + (stupica.d4 / 2 + 5), c_otv.x, c_otv.y - (stupica.d4 / 2 + 5), 3);
                            doc.ksLineSeg(c_otv.x - (stupica.d4 / 2 + 5), c_otv.y, c_otv.x + (stupica.d4 / 2 + 5), c_otv.y, 3);

                        }
                        else
                        {
                            doc.ksCircle(c_otv.x, c_otv.y, (stupica.d3 / 2), 1);
                            doc.ksLineSeg(c_otv.x, c_otv.y + (stupica.d3 / 2 + 5), c_otv.x, c_otv.y - (stupica.d3 / 2 + 5), 3);
                            doc.ksLineSeg(c_otv.x - (stupica.d3 / 2 + 5), c_otv.y, c_otv.x + (stupica.d3 / 2 + 5), c_otv.y, 3);
                        }
                        //как сделать обозначения повернутыми к центру пока не придумал, время и так нема
                    }

                    //////////   РАЗМЕРЫ
                    //диаметры

                    Diam_Razmer(0, 0, (stupica.D_okr / 2), (int)((stupica.D_external - stupica.D_okr) / 2 * multiply) + 15, 1, 1, 45, doc);
                    Diam_Razmer(list_of_holes[0].x, list_of_holes[0].y, (stupica.d4 / 2), 5, 2, 1, 45, doc);
                    Diam_Razmer(list_of_holes[1].x, list_of_holes[1].y, (stupica.d3 / 2), 5, 2, 1, 45, doc);

                    LinRazm(doc,
                        -(stupica.B / 2), (stupica.h - stupica.d / 2 - stupica.R),
                        (stupica.B / 2), (stupica.h - stupica.d / 2 - stupica.R),
                        2, 0, 12, 0);  //l1

                    LinRazm(doc,
                        (0), -(stupica.d / 2),
                        (0), (stupica.h - stupica.d / 2),
                        1, (stupica.d1 / 2) * multiply + 10, 0, 0);  //l1
                }
            }


        }





        private void Diam_Razmer(double xc, double yc, double rad, int dlina_vinosnoi_linii, short tip_strelki,
            int napr_vinosnoi_linii, int angle_vinosnoi_linii, ksDocument2D doc)
        {
            ksRDimParam aDim = (ksRDimParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_RDimParam);
            ksTextLineParam textLine = (ksTextLineParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_TextLineParam);
            ksTextItemParam textItem = (ksTextItemParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_TextItemParam);
            if (aDim == null || textLine == null || textItem == null)
                return;

            textLine.Init();
            textItem.Init();

            ksDimTextParam tPar = (ksDimTextParam)aDim.GetTPar();
            ksTextItemFont font = (ksTextItemFont)textItem.GetItemFont();
            ksDynamicArray arra = (ksDynamicArray)textLine.GetTextItemArr();
            ksRDimSourceParam sPar = (ksRDimSourceParam)aDim.GetSPar();
            ksRDimDrawingParam dPar = (ksRDimDrawingParam)aDim.GetDPar();
            if (tPar == null || font == null || sPar == null || dPar == null)
                return;

            tPar.Init(true);
            tPar.SetBitFlagValue(ldefin2d._AUTONOMINAL, true);
            tPar.sign = 1; //знак диаметра


            /*
                    font.Init();
                    font.height = 5;
                    font.ksu = 1;
                    font.fontName = "GOST type A";
                    font.SetBitVectorValue(ldefin2d.NEW_LINE, true);
                    */

            arra.ksAddArrayItem(-1, textItem);

            ksDynamicArray arr1 = (ksDynamicArray)tPar.GetTextArr();
            if (arr1 == null)
                return;
            arr1.ksAddArrayItem(-1, textLine);

            sPar.Init();
            sPar.xc = xc;
            sPar.yc = yc;
            sPar.rad = rad;

            dPar.Init();
            dPar.textPos = dlina_vinosnoi_linii;
            dPar.pt1 = tip_strelki;
            dPar.pt2 = tip_strelki;
            dPar.shelfDir = napr_vinosnoi_linii;
            dPar.ang = angle_vinosnoi_linii;

            int obj = doc.ksDiamDimension(aDim);
        }

        private bool liniya_s_obrivom(Stupica master, double multiply, ksDocument2D doc)
        {
            ksLBreakDimParam param = (ksLBreakDimParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_LBreakDimParam);
            if (param == null)
                return true;

            ksBreakDimDrawing dPar = (ksBreakDimDrawing)param.GetDPar();
            ksLBreakDimSource sPar = (ksLBreakDimSource)param.GetSPar();
            ksDimTextParam tPar = (ksDimTextParam)param.GetTPar();

            if (dPar == null || sPar == null || tPar == null)
                return true;

            dPar.Init();
            dPar.textPos = 5;
            dPar.pl = false;
            dPar.pt = 1; //стрелка внутри
            dPar.angle = 90;
            dPar.length = 10;


            sPar.Init();
            //точка размер чего строим
            sPar.x1 = master.l - master.b1;
            sPar.y1 = -(master.d / 2);
            //точки размерной линии
            sPar.x2 = master.l + 15 / multiply;
            sPar.y2 = -(master.d / 2);
            sPar.x3 = master.l + 15 / multiply;
            sPar.y3 = 0;

            tPar.Init(false);
            tPar.bitFlag = 0; //выводится только номинал
            tPar.style = 3; //текст размерной надписи????
            tPar.stringFlag = false;
            tPar.sign = 1; //diametr

            ksChar255 str = (ksChar255)_kompas.GetParamStruct((short)StructType2DEnum.ko_Char255);
            ksDynamicArray arrText = (ksDynamicArray)tPar.GetTextArr();

            if (str == null || arrText == null)
                return true;

            str.str = master.d.ToString();
            arrText.ksAddArrayItem(-1, str);

            int obj = doc.ksLinBreakDimension(param); //отрисовка
            return false;
        }

        private void set_rad_razm(Stupica master, ksDocument2D doc)
        {
            ksRDimParam aDim = (ksRDimParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_RDimParam);
            ksTextLineParam textLine = (ksTextLineParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_TextLineParam);
            ksTextItemParam textItem = (ksTextItemParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_TextItemParam);


            textLine.Init();
            textItem.Init();

            ksDimTextParam tPar = (ksDimTextParam)aDim.GetTPar();
            ksTextItemFont font = (ksTextItemFont)textItem.GetItemFont();
            ksDynamicArray arra = (ksDynamicArray)textLine.GetTextItemArr();
            ksRDimSourceParam sPar = (ksRDimSourceParam)aDim.GetSPar();
            ksRDimDrawingParam dPar = (ksRDimDrawingParam)aDim.GetDPar();


            tPar.Init(true);
            tPar.SetBitFlagValue(ldefin2d._AUTONOMINAL, true);
            tPar.sign = 3; //знак радиуса


            font.Init();
            font.height = 5;
            font.ksu = 1;
            font.fontName = "GOST type A";
            font.SetBitVectorValue(ldefin2d.NEW_LINE, true);

            arra.ksAddArrayItem(-1, textItem);


            sPar.Init();
            sPar.xc = (master.l1 + master.R1);
            sPar.yc = (master.d1 / 2 + master.R1);
            sPar.rad = (master.R1);

            dPar.Init();
            dPar.textPos = -15;
            dPar.pt1 = 1; //1 -стрелка изнутри 2-стрелка снаружи 0-нетс стрелки
            dPar.pt2 = 0;
            dPar.shelfDir = 1;
            dPar.ang = 180 + 45;

            int obj = doc.ksRadDimension(aDim);
        }

        void LinRazm(ksDocument2D doc, double x1, double y1, double x2, double y2, short gde_strelka, double vect_x, double vect_y, int type_znaka, int smeschenie_texta = 0)
        {

            ksLDimParam param = (ksLDimParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_LDimParam);
            if (param == null)
                return;

            ksDimDrawingParam dPar = (ksDimDrawingParam)param.GetDPar();
            ksLDimSourceParam sPar = (ksLDimSourceParam)param.GetSPar();
            ksDimTextParam tPar = (ksDimTextParam)param.GetTPar();

            if (dPar == null || sPar == null || tPar == null)
                return;

            dPar.Init();
            dPar.textPos = smeschenie_texta;
            dPar.textBase = 10;
            dPar.pt1 = gde_strelka;   //стрелка внутри
            dPar.pt2 = gde_strelka;
            dPar.ang = 30;
            dPar.lenght = 20;


            sPar.Init();
            sPar.basePoint = 1;
            sPar.ps = 2;
            sPar.x1 = x1;
            sPar.y1 = y1;
            sPar.x2 = x2;
            sPar.y2 = y2;
            sPar.dx = vect_x;
            sPar.dy = vect_y;

            tPar.Init(false);
            tPar.SetBitFlagValue(ldefin2d._AUTONOMINAL, true);
            tPar.SetBitFlagValue(ldefin2d._PREFIX, true);
            tPar.SetBitFlagValue(ldefin2d._DEVIATION, true);
            tPar.SetBitFlagValue(ldefin2d._UNIT, true);
            tPar.SetBitFlagValue(ldefin2d._SUFFIX, true);
            tPar.sign = type_znaka;
            

            int obj = doc.ksLinDimension(param);    //отрисовка
        }

        void Set_into_Stamp(ksDocument2D doc, string text)
        {
            ksTextItemParam itemParam = (ksTextItemParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_TextItemParam);
            if (itemParam != null)
            {
                itemParam.Init();

                ksTextItemFont itemFont = (ksTextItemFont)itemParam.GetItemFont();
                if (itemFont != null)
                {
                    itemFont.SetBitVectorValue(ldefin2d.NEW_LINE, true);
                    itemParam.s = text;
                    doc.ksTextLine(itemParam);
                }
            }
        }

        ksMathPointParam line_cirle_itersection(double R, double xl)
        {
            //отдаю только верхнее пересечение, потому как ... компас не работает а мне лень
            ksMathPointParam p1 = (ksMathPointParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_MathPointParam);

            //x^2+y^2=R^2
            //x=1
            //y^2=R^2-1^2

            var y = Math.Sqrt(Math.Pow(R, 2) - Math.Pow(xl, 2));

            p1.x = xl;
            p1.y = y;

            return p1;
        }

        ksMathPointParam point_by_angle(double R, double angle)
        {
            ksMathPointParam p1 = (ksMathPointParam)_kompas.GetParamStruct((short)StructType2DEnum.ko_MathPointParam);
            double x = Math.Cos(2 * Math.PI * angle / 360) * R;
            double y = Math.Sin(2 * Math.PI * angle / 360) * R;

            p1.x = x;
            p1.y = y;
            return p1;
        }
    }
}
