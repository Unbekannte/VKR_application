using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using KAPITypes;
using Kompas6API5;
using Kompas6Constants;

namespace WindowsFormsApplication2
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

            /*    
            info += $"\n_status = {_status}\n" +
                        $"_libraryId = {_libraryId}\n" +
                              $"_hWnd = {_hWnd}\n" +
                              $"";
                              */
            return info;
        }

        public void updateStatus(string st = null)
        {
            if (st != null)
            {
                _status = st;
            }
            // label3.Text = $"_status = {_status}";
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
                // throw;
            }
            var ss = parts[1].VariableCollection();
            //_kompas.ksMessage()
        }






        public List<kompasVariable> GetVarsOfAssembly()
        {
            if (_kompas == null)
            {
                LoadKompas();
                // MessageBox.Show("КОМПАС-3D не запущен!");
                // return null;
            }

            ksDocument3D doc = _kompas.ActiveDocument3D();

            if (doc == null)
            {
                MessageBox.Show("Файл сборки не открыт!");
                return null;
            }
            /*
            var parts = doc.PartCollection(true);
            
            List<string> names = new List<string>();

            var countOfParts = parts.GetCount;

            for (int i = 0; i < countOfParts; i++)
            {
                names.Add(parts.GetByIndex(i).name);
            }
            */

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

            //Variables = Variables.OrderBy(o => o.name).ToList();

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





    }
}
