using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using System.Windows.Forms;

namespace BlackListInput
{
    public partial class Form1 : Form
    {
        //テキストボックスに入力した値
        string inputString;

        public Form1()
        {
            InitializeComponent();
        }

        private void blackListButton_Click(object sender, EventArgs e)
        {
            string blackListFilePath = @"C:\Users\sato_\Downloads\2.ブラックリスト\ブラックリスト.xlsx";
            string blackList2FilePath = @"C:\Users\sato_\Downloads\2.ブラックリスト\BlackList.csv";

            Cursor.Current = Cursors.WaitCursor;
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            //Excelが開かないようにする
            xlApp.Visible = false;
            //指定したパスのExcelを起動
            Workbook wb = xlApp.Workbooks.Open(Filename: blackListFilePath);

            for (int i = 0; i < inputTextBox.Lines.Length; i++)
            {
                getInputTextBox(i);

                inputToExcel(xlApp, wb, inputString);
                
                InputCsvFile(blackList2FilePath, inputString);
            }

            wb.Close(true);

            xlApp.Quit();

            Cursor.Current = Cursors.Default;

            //テキストボックスの表示を空にする
            inputTextBox.Text = "";
        }

        private void blandButton_Click(object sender, EventArgs e)
        {
            string blandFilePath = @"C:\Users\sato_\Downloads\1.出品テンプレートファイル\出品禁止ブランド、キーワード.xlsx";
            string blandFilePathCsv = @"C:\Users\sato_\Downloads\1.出品テンプレートファイル\出品禁止ブランド、キーワード.csv";
            Cursor.Current = Cursors.WaitCursor;
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            //Excelが開かないようにする
            xlApp.Visible = false;
            //指定したパスのExcelを起動
            Workbook wb = xlApp.Workbooks.Open(Filename: blandFilePath);

            for (int i = 0; i < inputTextBox.Lines.Length; i++)
            {
                getInputTextBox(i);

                inputToExcel(xlApp, wb, inputString);

                InputCsvFile(blandFilePathCsv, inputString);
            }

            //Appを閉じる
            wb.Close(true);

            xlApp.Quit();

            Cursor.Current = Cursors.Default;

            //テキストボックスの表示を空にする
            inputTextBox.Text = "";
        }

        //テキストボックスの文字列を取得する
        public void getInputTextBox(int TextLine)
        {
            inputString = inputTextBox.Lines[TextLine].Trim();
        }

        //エクセルにテキストボックスの情報を書き込む
        public void inputToExcel(Microsoft.Office.Interop.Excel.Application xlApp, Workbook wb, string inputObject)
        {
            try
            {
                //Sheetを指定
                ((Worksheet)wb.Sheets[2]).Select();
            }
            catch (Exception ex)
            {
                //Sheetがなかった場合のエラー処理

                //Appを閉じる
                wb.Close(false);
                xlApp.Quit();

                //Errorメッセージ
                Console.WriteLine("指定したSheetは存在しません．");
                Console.ReadLine();

                //実行を終了
                System.Environment.Exit(0);
            }
            //変数宣言
            Range CellRange;

            //最終行を取得
            var yCount  = wb.Sheets[2].UsedRange.Rows.Count;

            //書き込む場所を指定
            CellRange = xlApp.Cells[++yCount, 1] as Range;

            //書き込む内容
            CellRange.Value2 = inputObject;
        }

        //csvファイルにデータを書き込む
        public void InputCsvFile(string CsvFilePath, string BlackAsin)
        {
            using (var CsvWriteStream = new StreamWriter(CsvFilePath, true, System.Text.Encoding.GetEncoding("sjis")))
            {
                CsvWriteStream.WriteLine(BlackAsin);
            }
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            //垂直、水平両方のスクロールバーを表示
            inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        }
    }
}
