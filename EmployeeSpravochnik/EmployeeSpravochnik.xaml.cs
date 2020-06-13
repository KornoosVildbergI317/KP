using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;

namespace ZooMail.EmployeeSpravochnik
{
    /// <summary>
    /// Логика взаимодействия для EmployeeSpravochnik.xaml
    /// </summary>
    public partial class EmployeeSpravochnik : Window
    {
        List<Models.ModelEmployee> Employees;

        public string extension = string.Empty;
        private string QR = "";
        public EmployeeSpravochnik()
        {
            InitializeComponent();


        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Employees = (new DBManager()).getEmployeeList();
            dataGridEmployeeSpravichnic.ItemsSource = Employees;
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridEmployeeSpravichnic.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбран сотрудник для редактирования");
                return;
            }

            var ap = new DBManager().getEmployeeList();

            Models.ModelEmployee modelEmployee = null;
            foreach (var it in ap)
            {
                if (it.ID_Authorization == Employees[selectedIndex].ID_Authorization)
                {
                    modelEmployee = it;
                    break;
                }
            }

            var ce = new EmployeeSpravochnikEdit(modelEmployee);
            ce.ShowDialog();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var ce = new EmployeeSpravochnikEdit(null);
            ce.ShowDialog();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridEmployeeSpravichnic.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбран сотрудник для удаления");
                return;
            }


            if (MessageBox.Show("Вы уверены?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }



            new DBManager().deleteEmployee(Employees[selectedIndex].ID_Authorization);
            MessageBox.Show("Операция выполнена");
            Employees = (new DBManager()).getEmployeeList();
            dataGridEmployeeSpravichnic.ItemsSource = Employees;
        }

        private void createExportDoc()
        {
            try
            {


                DBManager con = new DBManager();

                var modelEmployee = con.getEmployeeList();

                if (extension == string.Empty)
                {
                    MessageBox.Show("Не выбран тип экспортруемого файла");
                    return;
                }


                switch (extension)
                {
                    case (".docx"):
                        string pathDocumentDOCX = Session.baseDir + "Сотрудники" + extension;
                        DocX document = DocX.Create(pathDocumentDOCX);
                        Xceed.Document.NET.Paragraph paragraph = document.InsertParagraph();
                        paragraph.
                            AppendLine("Документ '" + "Отчет о сотрудниках" + "' создан " + DateTime.Now.ToShortDateString()).
                            Font("Time New Roman").
                            FontSize(16).Bold().Alignment = Alignment.left;

                        paragraph.AppendLine();
                        Xceed.Document.NET.Table doctable = document.AddTable(modelEmployee.Count + 1, 2);
                        doctable.Design = TableDesign.TableGrid;
                        doctable.TableCaption = "Сотрудники";

                        doctable.Rows[0].Cells[0].Paragraphs[0].Append("Сотрудники").Font("Times New Roman").FontSize(14);

                        for (int i = 0; i < modelEmployee.Count; i++)
                        {
                            doctable.Rows[i + 1].Cells[0].Paragraphs[0].Append(modelEmployee[i].Surname).Font("Times New Roman").FontSize(14);
                        }
                        document.InsertParagraph().InsertTableAfterSelf(doctable);
                        document.Save();
                        MessageBox.Show("Отчет успешно сформирован!");
                        Process.Start(pathDocumentDOCX);
                        break;

                    case (".xlsx"):
                        Excel.Application excel;
                        Excel.Workbook worKbooK;
                        Excel.Worksheet worKsheeT;
                        Excel.Range celLrangE;

                        string pathDocumentXLSX = Session.baseDir + "Сотрудники" + extension;

                        try
                        {
                            excel = new Excel.Application();
                            excel.Visible = false;
                            excel.DisplayAlerts = false;
                            worKbooK = excel.Workbooks.Add(Type.Missing);


                            worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                            worKsheeT.Name = "Сотрудники";

                            worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 8]].Merge();
                            worKsheeT.Cells[1, 1] = "Сотрудники";
                            worKsheeT.Cells.Font.Size = 15;

                            for (int i = 0; i < modelEmployee.Count; i++)
                            {
                                worKsheeT.Cells[i + 3, 1] = modelEmployee[i].Surname;
                            }

                            worKbooK.SaveAs(pathDocumentXLSX); ;
                            worKbooK.Close();
                            excel.Quit();
                            MessageBox.Show("Отчет успешно сформирован!");
                            Process.Start(pathDocumentXLSX);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                        finally
                        {
                            worKsheeT = null;
                            celLrangE = null;
                            worKbooK = null;
                        }

                        break;


                    case (".pdf"):
                        string pathDocumentPDF = Session.baseDir + "Сотрудники" + extension;
                        if (File.Exists(Session.baseDir + "Сотрудники.docx"))
                        {
                            Word.Application appWord = new Word.Application();
                            var wordDocument = appWord.Documents.Open(Session.baseDir + "Сотрудники.docx");
                            wordDocument.ExportAsFixedFormat(pathDocumentPDF, Word.WdExportFormat.wdExportFormatPDF);
                            MessageBox.Show("Отчет успешно сформирован!");
                            wordDocument.Close();
                            Process.Start(pathDocumentPDF);
                        }
                        else
                            MessageBox.Show("Сначала сформируйте отчет .docx");
                        break;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Отсутсвие Ms Office на компьютере. Пожалуйста скачайте его.");
                Process.Start("https://www.microsoft.com/ru-ru/microsoft-365/compare-all-microsoft-365-products?tab=1&rtc=1");
            }
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(createExportDoc));
            t.Start();
        }

        private void ComboBoxExport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)comboBoxExport.SelectedItem;
            extension = typeItem.Content.ToString();



        }
    }
}
