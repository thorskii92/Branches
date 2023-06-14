using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchesProgram
{
    class Branch
    {
        public string Name { get; set; }
        public Branch[] SubBranches { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of branches to create: ");
            int numBranches = int.Parse(Console.ReadLine());
            Branch root = CreateBranchStructure(numBranches);
            DisplayBranchStructure(root, "");
            int depth = CalculateDepth(root);
            Console.WriteLine("Depth: " + depth);
            Console.ReadLine();
        }

        static Branch CreateBranchStructure(int numBranches)
        {
            Branch root = new Branch { Name = "O" };

            for (int i = 0; i < numBranches; i++)
            {
                Branch branch = new Branch { Name = "O" };
                CreateSubBranches(branch);
                root.SubBranches = AddBranchToArray(root.SubBranches, branch);
            }

            return root;
        }

        static void CreateSubBranches(Branch branch)
        {
            Console.Write("Enter the number of sub-branches for branch " + branch.Name + " (0 for no sub-branches): ");
            int numSubBranches = int.Parse(Console.ReadLine());

            if (numSubBranches > 0)
            {
                branch.SubBranches = new Branch[numSubBranches];

                for (int i = 0; i < numSubBranches; i++)
                {
                    Branch subBranch = new Branch { Name = "O" };
                    CreateSubBranches(subBranch);
                    branch.SubBranches[i] = subBranch;
                }
            }
        }

        static Branch[] AddBranchToArray(Branch[] array, Branch branch)
        {
            if (array == null)
            {
                return new Branch[] { branch };
            }
            else
            {
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = branch;
                return array;
            }
        }

        static void DisplayBranchStructure(Branch branch, string indent)
        {
            if (branch == null)
            {
                return;
            }

            Console.WriteLine(indent + branch.Name);

            if (branch.SubBranches != null)
            {
                string subIndent = indent + "  ";

                for (int i = 0; i < branch.SubBranches.Length; i++)
                {
                    if (i < branch.SubBranches.Length - 1)
                    {
                        Console.Write(subIndent.Substring(0, subIndent.Length - 2));
                        Console.Write("/ ");
                    }
                    else
                    {
                        Console.Write(subIndent.Substring(0, subIndent.Length - 2));
                        Console.Write("\\ ");
                    }

                    DisplayBranchStructure(branch.SubBranches[i], subIndent);
                }
            }
        }

        static int CalculateDepth(Branch branch)
        {
            if (branch.SubBranches == null || branch.SubBranches.Length == 0)
            {
                return 1;
            }
            else
            {
                int maxDepth = 0;
                foreach (Branch subBranch in branch.SubBranches)
                {
                    int subBranchDepth = CalculateDepth(subBranch);
                    if (subBranchDepth > maxDepth)
                    {
                        maxDepth = subBranchDepth;
                    }
                }
                return maxDepth + 1;
            }
        }
    }
}