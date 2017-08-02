using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace LoanCalculator
{
    public class DataImporter
    {
        public List<Lender> ImportLenderData(string filepath)
        {
            using (var streamReader = new StreamReader(filepath))
            {
                CsvReader csvReader;
                try
                {
                    csvReader = new CsvReader(streamReader);
                    csvReader.ReadHeader();
                    ValidateHeaders(csvReader.FieldHeaders);

                }
                catch (Exception e)
                {
                    //TODO: Log header error
                    throw;
                }

                var rowIndex = 0;
                //var lenderList = new SortedDictionary<decimal,Lender>();
                var lenderList = new List<Lender>();

                while (csvReader.Read())
                {
                    try
                    {
                        rowIndex++;
                        var nameField = csvReader.GetField<string>(0);
                        var rateField = csvReader.GetField<decimal>(1);
                        var amountField = csvReader.GetField<decimal>(2);
                        lenderList.Add(new Lender()
                        {
                            Name = nameField,
                            Rate = rateField,
                            Amount = amountField
                        });
                    }
                    catch (Exception e)
                    {
                        //Lor parsing error
                        throw;
                    }
                }
                return lenderList;

            }
        }


        private void ValidateHeaders(string[] fieldHeaders)
        {
            var lenderNameHeaderFormat = "LENDER";
            var rateHeaderFormat = "RATE";
            var amountAvailableHeaderFormat = "AVAILABLE";

            if (!string.Equals(fieldHeaders[0].ToUpper(), lenderNameHeaderFormat))
                throw new CsvHeaderException(fieldHeaders[0].ToUpper(), lenderNameHeaderFormat);

            if (!string.Equals(fieldHeaders[1].ToUpper(), rateHeaderFormat))
                throw new CsvHeaderException(fieldHeaders[1].ToUpper(), rateHeaderFormat);

            if (!string.Equals(fieldHeaders[2].ToUpper(), amountAvailableHeaderFormat))
                throw new CsvHeaderException(fieldHeaders[2].ToUpper(), amountAvailableHeaderFormat);
        }

        public class CsvHeaderException : CsvReaderException
        {
            public CsvHeaderException(string invalidHeader, string expectedHeaderFormat)
                : base("Header has an invalid format. Given value was: " + invalidHeader +
                       " . Expected value is: " + expectedHeaderFormat)
            {

            }
        }
    }
}
