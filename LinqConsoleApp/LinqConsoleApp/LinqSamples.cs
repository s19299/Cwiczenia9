using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            
            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var resSQL = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };
            
            Console.WriteLine("SQL result: ");
            foreach (var emp in resSQL)
            {
                Console.WriteLine(emp);
            }


            //2. Lambda and Extension methods
            var resLambda = Emps.Where(emp => emp.Job.Equals("Backend Programmer"));

            Console.WriteLine("Lambda result: ");
            foreach (var emp in resLambda)
            {
                Console.WriteLine(emp);
            }

        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            
            //SQL
            var resSQL = from emp in Emps
                where emp.Job == "Frontend programmer" && emp.Salary > 1000 orderby emp.Salary descending

                select new
                {
                    Nazwisko = emp.Ename,
                    Zawod = emp.Job
                };
            
            Console.WriteLine("SQL result: ");
            foreach (var emp in resSQL)
            {
                Console.WriteLine(emp);
            }
            
            
            //=>

            var resLambda = Emps.Where(emp => emp.Job.Equals("Frontend Programmer") && emp.Salary > 1000)
                .OrderByDescending(emp => emp.Salary);

            Console.WriteLine("Lambda result: ");
            foreach (var emp in resLambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {

            //SQL
            int maxSalary = (from emp in Emps select emp.Salary).Max();
            var resSQL = from emp in Emps
                where emp.Salary == maxSalary
                select new
                {
                    Salary = emp.Salary
                };

            Console.WriteLine("SQL result: ");
            
                Console.WriteLine(resSQL);

            var resLambda = Emps.Max(emp => emp.Salary);
                
            Console.WriteLine("Lambda result: ");
            
                Console.WriteLine(resLambda);

        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
       
        public void Przyklad4()
        {
            var resSQL = from emp in Emps
                where emp.Salary == (from emp1 in Emps select emp1.Salary).Max()
                select new
                {
                    Nazwisko = emp.Ename,
                    Salary = emp.Salary
                };

            Console.WriteLine("SQL result: ");
            
            foreach (var emp in resSQL)
            {
                Console.WriteLine(emp);
            }

            var resLambda = Emps.Where(emp => emp.Salary == Emps.Select(emp1 => emp1.Salary).Max());

            Console.WriteLine("Lambda result:");
            foreach (var emp in resLambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            var resSQL = from emp in Emps
                select new
                {
                    Nazwisko = emp.Ename,
                    Praca = emp.Job
                };

            Console.WriteLine("SQL result: ");
            foreach (var emp in resSQL)
            {
                Console.WriteLine(emp);
            }

            var resLambda = Emps.Select(emp => new {Nazwisko = emp.Ename, Praca = emp.Job});
            
            Console.WriteLine("Lambda result: ");
            foreach (var emp in resLambda)
            {
                Console.WriteLine(emp);
            }

        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {

            var resSQL = from emp in Emps
                join dept in Depts on emp.Deptno equals dept.Deptno
                select new
                {
                    emp,
                    dept
                };

            Console.WriteLine("SQL result");
            foreach (var emp in resSQL)
            {
                Console.WriteLine(emp);
            }

            var resLambda = Emps.Join(Depts, dept => dept.Deptno, emp => emp.Deptno, (emp, dept) => new {emp, dept});

            Console.WriteLine("Lambda result");
            foreach (var emp in resLambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {

            var resSQL = from emp in Emps
                group emp by emp.Job
                into empByJob
                select new
                {
                    Job = empByJob.Key,
                    EmployeesNumber = empByJob.Count()
                };

            Console.WriteLine("SQL result");
            foreach (var emp in resSQL)
            {
                Console.WriteLine(emp);
            }

            var resLambda = Emps.GroupBy(emp => emp.Job).Select(job => new {Praca = job.Key, EmployeeNumber = job.Count()});
            
            Console.WriteLine("Lambda result");
            foreach (var emp in resLambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {

            var resSQL = from emp in Emps
                where emp.Job == "Backend programmer"
                select new
                {
                    Nazwisko = emp.Ename
                };
            
            Console.WriteLine("SQL result");
            Console.WriteLine(resSQL.Any());

            var resLambda = Emps.Where(emp => emp.Job.Equals("Backend Programmer"))
                .Select(emp => new {Nazwisko = emp.Ename});
            
            Console.WriteLine("Lambda result");
            Console.WriteLine(resLambda.Any());
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {
            var resSQL = from emp in Emps
                where emp.Job == "Frontend programmer"
                orderby emp.HireDate descending
                select emp;
            
            Console.WriteLine("SQL result");
            Console.WriteLine(resSQL.FirstOrDefault());

            var resLambda = Emps.Where(emp => emp.Job.Equals("Frontend Programmer"))
                .OrderByDescending(emp => emp.HireDate).FirstOrDefault();
            
            Console.WriteLine("Lambda result");
            Console.WriteLine(resLambda);
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10Button_Click()
        {

            var resSQL = (from emp in Emps
                select new
                {
                    emp.Ename,
                    emp.Job,
                    emp.HireDate
                }).Union(from emp in Emps
                select new
                {
                    Ename = "Brak wartosci", 
                    Job = (string)null,
                    HireDate = (DateTime?)null
                });

            Console.WriteLine("SQL result");
            foreach (var emp in resSQL)
            {
                Console.WriteLine(emp);
            }

            var resLambda = Emps.Select(emp => (emp.Ename, emp.Job, emp.HireDate)).Union(Emps.Select(emp1 =>
                (emp1.Ename = "Brak wartosci", emp1.Job = (string) null, emp1.HireDate = (DateTime?) null)));

            Console.WriteLine("Lambda result: ");
            foreach (var emp in resLambda)
            {
                Console.WriteLine(emp);
            }

        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {

            var resSQL = (from emp in Emps
                select emp).Aggregate((emp, nextEmp) => emp.Salary > nextEmp.Salary ? emp : nextEmp);
            
            Console.WriteLine("SQL result: ");
            Console.WriteLine(resSQL);
            
            var resLambda = Emps.Aggregate((emp, nextEmp) => emp.Salary > nextEmp.Salary ? emp : nextEmp);
            
            Console.WriteLine("Lambda result: ");
            Console.WriteLine(resLambda);
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {

            var resSQL = from emp in Emps
                from dept in Depts
                select new
                {
                    emp,
                    dept
                };

            Console.WriteLine("SQL result: ");
            foreach (var cross in resSQL)
            {
                Console.WriteLine(cross);
            }

            var resLambda = Emps.SelectMany(emp => Depts, (emp1, dept) => new {emp1, dept});
            
            Console.WriteLine("Lambda result: ");
            foreach (var cross in resLambda)
            {
                Console.WriteLine(cross);
            }
        }
    }
}
