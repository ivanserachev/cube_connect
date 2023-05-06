using Microsoft.AnalysisServices.Tabular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cube_connect
{
    internal class TableProcessing
    {
        private Table table;
        private Partition part;

        public TableProcessing(Table table)
        {
            this.table = table;

        }

        public void get_partitions()
        {
            PartitionCollection partitions;

            int i = 1;

            Console.ForegroundColor = ConsoleColor.Cyan;

            try
            {
                partitions = table.Partitions;
                Console.WriteLine("\n---Table '{0}' partitions---", table.Name);
                foreach (var partition in partitions)
                {
                    Console.WriteLine("{0}. Partition:\t{1}", i, partition.Name);
                    if (partition.Name == "Partition")
                    {
                       
                        part = partition.Clone();
                
                        part.RequestRename("test");
     
                        Console.WriteLine(part.Name);

                    }
                    i++;

                }
                Console.WriteLine("---Table '{0}' partitions---", table.Name);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }

        }

        public void add_partition()
        {
            try
            {

                PartitionCollection cl = table.Partitions;
                foreach (var item in cl)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine(part.Name);
                table.Partitions.Add(part);
                Console.WriteLine("part added");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }

        }
    }
}
