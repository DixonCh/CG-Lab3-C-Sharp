using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGlab3
{
    //Define the class Point
    public class Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    //Define the class Line
    public class Line
    {
        public Point p1;
        public Point p2;
        public Line(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }

    //Doubly Linked List Class
    public class DLinkedList
    {
        private Point data;
        private DLinkedList next;
        private DLinkedList prev;

        public DLinkedList()

        {
            data = new Point(0, 0);
            next = null;
            prev = null;

        }
        public DLinkedList(Point value)
        {
            data = value;
            next = null;
            prev = null;
        }
        public DLinkedList(Point value, DLinkedList nxt)

        {
            data = value;
            next = nxt;
            prev = null;

        }

        //InsertPrevNext method is used for inserting next and previous other than last.next element
        public DLinkedList InsertPrevNext(Point value)

        {

            DLinkedList node = new DLinkedList(value);

            if (this.next == null)

            {
                node.prev = this;
                this.next = node;
            }
            return node;
        }
        //DLinkedList method is used for inserting only  next and previous other than last.next and first.prev element
        public DLinkedList InsertPrevNext(Point value, DLinkedList nxt)
        {
            DLinkedList node = new DLinkedList(value, nxt);
            node.prev = this;
            this.next = node;
            return node;

        }

        static void Main(string[] args)

        {
            DLinkedList[] nonode = polygonIntermsofLinkedLIst();
            int numberofnode = nonode.Count();
            int countforConvex = 0;
            for (int i = 0; i < numberofnode; i++)
            {
                countforConvex += CheckTurn(nonode[i].prev.data, nonode[i].data, nonode[i].next.data);
            }
            if (countforConvex == numberofnode)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("The Given Polygon is Convex Polygon.");
                Console.WriteLine("------------------------------------");
                string recheck = "Yes";
                while (recheck != "no")
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("Enter the point for point inclusion test:");
                    Console.WriteLine("-----------------------------------------");
                    string Input = Console.ReadLine();
                    string[] Number = Input.Split(',', ' ');
                    Point inclusionTestpoint = new Point(Convert.ToInt32(Number[0]), Convert.ToInt32(Number[1]));
                    pointInclusionForconvex(inclusionTestpoint, nonode);
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Do you want to check Another Point?");
                    Console.WriteLine("-----------------------------------");
                    recheck = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("The given polygon is not Convex Polygon.");
                Console.WriteLine("----------------------------------------");
                string recheck = "Yes";
                while (recheck != "no")
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("Enter the point for point inclusion test:");
                    Console.WriteLine("-----------------------------------------");
                    string Input = Console.ReadLine();
                    string[] Number = Input.Split(',', ' ');
                    Point inclusionTestpoint = new Point(Convert.ToInt32(Number[0]), Convert.ToInt32(Number[1]));
                    pointInclusionForNonconvex(inclusionTestpoint, nonode);
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Do you want to check another point?");
                    Console.WriteLine("-----------------------------------");
                    recheck = Console.ReadLine();
                }
            }
        }

        public static DLinkedList[] polygonIntermsofLinkedLIst()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Enter the number of Points in polygon: ");
            Console.WriteLine("---------------------------------------");
            int numberofnode = Convert.ToInt32(Console.ReadLine());
            Point[] pointofPolygon = new Point[numberofnode];
            for (int i = 0; i < numberofnode; i++)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("Enter the Point: ");
                Console.WriteLine("-----------------");
                string Input = Console.ReadLine();
                string[] Number = Input.Split(',', ' ');
                pointofPolygon[i] = new Point(Convert.ToInt32(Number[0]), Convert.ToInt32(Number[1]));
            }
            DLinkedList[] nonode = new DLinkedList[numberofnode];

            nonode[0] = new DLinkedList(pointofPolygon[0]);
            for (int i = 1; i < numberofnode; i++)
            {
                nonode[i] = nonode[i - 1].InsertPrevNext(pointofPolygon[i]);
            }
            nonode[0] = nonode[numberofnode - 1].InsertPrevNext(pointofPolygon[0], nonode[0].next);
            return nonode;
        }

        //Checking Turn Test
        public static int CheckTurn(Point p0, Point p1, Point p2)
        {
            int turn = 0;
            double area = ComputeArea(p0, p1, p2);
            Console.WriteLine("---------------------------------");
            Console.WriteLine("The area of Triangle is : " + area);
            Console.WriteLine("---------------------------------");

            if (area > 0)
            {
                turn = 1;
            }
            else if (area < 0)
            {
                turn = 0;
            }
            else
            {
                turn = 0;
            }
            return turn;
        }

        //Computing the Area
        public static double ComputeArea(Point p0, Point p1, Point p2)
        {
            double area = 0;
            area = 0.5 * (p0.x * p1.y + p0.y * p2.x + p1.x * p2.y - p0.x * p2.y - p0.y * p1.x - p2.x * p1.y);
            return area;
        }

        //Check Intersection between lines
        public static int LineIntersection(Line l1, Line l2)
        {
            int a = 0;
            //Area of Triangle pqr
            double pqr = ComputeArea(l1.p1, l1.p2, l2.p1);
            //Area of Triangle pqs
            double pqs = ComputeArea(l1.p1, l1.p2, l2.p2);
            //Area of Triangle rsp
            double rsp = ComputeArea(l2.p1, l2.p2, l1.p1);
            //Area of Triangle rsq
            double rsq = ComputeArea(l2.p1, l2.p2, l1.p2);
            //Checking the area of every triangle 
            if (pqr == 0 || pqs == 0 || rsp == 0 || rsq == 0)
            {
                if (pqr == 0)
                {
                    if (l1.p1.x <= l2.p1.x && l2.p1.x <= l1.p2.x)
                        a++;
                }
                else if (pqs == 0)
                {
                    if ((l1.p1.x <= l2.p2.x && l2.p2.x <= l1.p2.x))
                        a++;
                }
                else if (rsp == 0)
                {
                    if ((l2.p1.x <= l1.p1.x && l1.p1.x <= l2.p2.x))

                        a++;
                }
                else
                {
                    if ((l2.p1.x <= l1.p2.x && l1.p2.x <= l2.p2.x))

                        a++;
                }

            }
            else if (((pqr > 0 && pqs < 0) && (rsp > 0 && rsq < 0)) || ((pqr > 0 && pqs < 0) && (rsp < 0 && rsq > 0)) || ((pqr < 0 && pqs > 0) && (rsp < 0 && rsq > 0)) || ((pqr < 0 && pqs > 0) && (rsp > 0 && rsq < 0)))
            {
                a++;
            }
            return a;
        }

        //Method for Point Inclusion Test
        public static void pointInclusionForconvex(Point inclusionTestpoint, DLinkedList[] node)
        {
            int nodeCount = node.Count();
            int countforConvex = 0;
            for (int i = 0; i < nodeCount; i++)
            {
                countforConvex += CheckTurn(inclusionTestpoint, node[i].data, node[i].next.data);
            }
            if (countforConvex == nodeCount)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("The point lies inside the Polygon.");
                Console.WriteLine("-------------------------------------");
            }
            else
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("The point lies outside the Polygon.");
                Console.WriteLine("--------------------------------------");
            }
        }


        public static void pointInclusionForNonconvex(Point inclusionTestpoint, DLinkedList[] node)
        {
            int nodeCount = node.Count();
            int countforConvex = 0;

            for (int i = 0; i < nodeCount; i++)
            {
                Line l1 = new Line(inclusionTestpoint, new Point(2000, 2000));
                Line l2 = new Line(node[i].data, node[i].next.data);
                countforConvex += LineIntersection(l1, l2);
            }

            if (countforConvex % 2 == 1)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("The point lies inside the Polygon.");
                Console.WriteLine("----------------------------------");
            }
            else
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("The point lies outside the Polygon.");
                Console.WriteLine("-----------------------------------");
            }
        }

    }
}

