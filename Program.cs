using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBasicTrainingExercise
{
    class Program
    {
        enum ATTR
        {
            JOB,
            STACKUP,
            STACKUP_SEG,
            NONE
        }
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("AUTOM_DEMO1.xml", Encoding.Default);
            StreamWriter sw = new StreamWriter("rslt.txt", false, Encoding.Default);
            


            string line;
            string[] jobAttrs = new string[100];
            string[] jobVals = new string[100];
            string[] stackupAttrs = new string[100];
            string[] stackupVals = new string[100];
            string[] stackupSegAttrs = new string[500];
            string[] stackupSegVals = new string[500];
            string[] words;
            ATTR LINE = ATTR.NONE;
            int i = 0, j = 0, k = 0; 
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                //Console.WriteLine(line);
               // sw.WriteLine(line);

                line = line.Trim();
                if(line.StartsWith("<JOB NAME"))
                {
                    LINE = ATTR.JOB; 
                    words = line.Split(new char[] { ' ', '=' });
              
                    Console.WriteLine(line);
                    sw.WriteLine(words[0].Trim('<')+":"+words[1]+"="+words[2]);

                    continue;
                }
                if (line.StartsWith("<STACKUP NAME"))
                {
                    LINE = ATTR.STACKUP;
                    words = line.Split(new char[] { ' ', '=' });

                    Console.WriteLine(line);
                    sw.WriteLine(words[0].Trim('<') + ":" + words[1] + "=" + words[2]);

                    continue;
                }
                if (line.StartsWith("<STACKUP_SEG NAME"))
                {
                    LINE = ATTR.STACKUP_SEG;
                    words = line.Split(new char[] { ' ', '=' });

                    Console.WriteLine(line);
                    sw.WriteLine(words[0].Trim('<') + ":" + words[1] + "=" + words[2]+" "+words[3]);

                    continue;
                }

                if (line.StartsWith(">"))
                {
                    switch (LINE)
                    {
                        case ATTR.JOB:
                            sw.Write("{");
                            for (int jobi = 0; jobi < jobAttrs.Length; jobi++)
                            {
                                // { {“ATTR1”,”Val1”},{“ATTR2”,”Val2”},...}
                                if ( !String.IsNullOrEmpty(jobAttrs[jobi]))
                                {
                                    sw.Write("{\""+jobAttrs[jobi] +"\","+jobVals[jobi] +"}");
                                    int nexti = jobi +1; 
                                    if (!String.IsNullOrEmpty(jobAttrs[nexti]))
                                    {
                                        sw.Write(",");
                                    }

                                }
                                                            
                            }
                            sw.WriteLine("}");
                            break;
                        case ATTR.STACKUP:
                            sw.Write("{");
                            for (int jobi = 0; jobi < stackupAttrs.Length; jobi++)
                            {
                                // { {“ATTR1”,”Val1”},{“ATTR2”,”Val2”},...}
                                if (!String.IsNullOrEmpty(stackupAttrs[jobi]))
                                {
                                    sw.Write("{\"" + stackupAttrs[jobi] + "\"," + stackupVals[jobi] + "}");
                                    int nexti = jobi + 1;
                                    if (!String.IsNullOrEmpty(stackupAttrs[nexti]))
                                    {
                                        sw.Write(",");
                                    }
                                }

                            }
                            sw.WriteLine("}");
                            break;
                        case ATTR.STACKUP_SEG:
                            sw.Write("{");
                            for (int jobi = k-36; jobi < k; jobi++)
                            {
                                // { {“ATTR1”,”Val1”},{“ATTR2”,”Val2”},...}
                                if (!String.IsNullOrEmpty(stackupSegAttrs[jobi]))
                                {
                                    sw.Write("{\"" + stackupSegAttrs[jobi] + "\"," + stackupSegVals[jobi] + "}");
                                    int nexti = jobi + 1;
                                    if (!String.IsNullOrEmpty(stackupSegAttrs[nexti]))
                                    {
                                        sw.Write(",");
                                    }
                                }

                            }
                            sw.WriteLine("}");
                            break;
                        case ATTR.NONE:
                            break;
                        default:
                            break;
                    }

                    LINE = ATTR.NONE;
                    continue; 
                }

                switch (LINE)
                {
                    case ATTR.JOB:
                        words = line.Split('=');
                        jobAttrs[i] = words[0];
                        jobVals[i] = words[1];
                        i++;
                        Console.WriteLine(line);
                        break;
                    case ATTR.STACKUP:
                         words = line.Split('=');
                        stackupAttrs[j] = words[0];
                        stackupVals[j] = words[1];
                        j++;
                        Console.WriteLine(line);
                        break;
                    case ATTR.STACKUP_SEG:
                        words = line.Split('=');
                        stackupSegAttrs[k] = words[0];
                        stackupSegVals[k] = words[1];
                        k++;
                        Console.WriteLine(line);
                        break;
                    default:
                        break;
                }

    


            }
            sw.Close();


            Console.ReadKey();


        }
    }
}
