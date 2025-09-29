namespace LinkedList
{
    public class ListManipFirst
    {
        private Node _headFirst = null;
        private Node _headLast = null;
        private String _options = "options are:\n0 - exit\n1 - add a node\n2 - remove a node\n3 - look at one node\n4 - look at the list\n";

        public ListManipFirst()
        {
           createInitialList();
            menu();
        }

        private void createInitialList()
        {
            String[] initializeFirstNames = { "one","two", "three","four","five","six","seven","eight", "nine", "ten",
                "eleven","twelve","thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen",
                    "twenty", "twenty-one", "twenty-two", "twenty-three", "twenty-four" };
           
            String[] initializeLastNames = { "alpha","beta", "gamma", "delta", "epsilon", "zeta", "eta", "theta", 
                "iota", "kappa", "lambda", "mu", "nu", "xi", "omicron", "pi", "rho", "sigma", "tau", "upsilon", "phi",
                "chi", "psi", "omega" };

            for (int n = 0; n < initializeFirstNames.Length; n++)
            {
                Node temp = createNewNode();
                initializeNode(temp, initializeFirstNames[n], initializeLastNames[n]);
                insertNode(temp);
            }
        }

        private void menu()
        {
            InputOutput inOut = new InputOutput();
            String firstName;
            Node theNode;

            while (true)
            {
                String choice = inOut.obtainDataFromUser(_options);
                
                if (canBePositiveInt(choice))
                {
                    switch (Convert.ToInt32(choice))
                    {
                        case 0://exit
                            Environment.Exit(0);
                            break;
                        case 1://add one node
                            Node temp = createNewNode();
                            initializeNode(temp, inOut);
                            insertNode(temp);
                            inOut.skipOneLine();
                            break;
                        case 2://remove one node
                            firstName = nameToFind(inOut, "please input the name of the item to remove  ").ToLower();
                            theNode = findNode(firstName);

                            if (theNode == null)
                                inOut.displayMessage("\nthe node called " + firstName + " cannot be removed because it is not in the list\n");
                            else
                            {
                                removeNode(theNode);
                                inOut.displayMessage("\nthe node containing the name " + firstName + " has been deleted\n");
                            }

                            break;
                        case 3://look at one node
                            firstName = nameToFind(inOut, "please input the name of the item to find  ").ToLower();
                            theNode = findNode(firstName);

                            if (!(theNode == null))
                                inOut.displayMessage(lookAtOneNode(theNode, firstName));
                            else
                                inOut.displayMessage("\nthe node called " + firstName + " is not in the list\n");
                            break;
                        case 4://look at the list
                            inOut.displayMessage(lookAtTheList());
                            break;
                        default:
                            inOut.displayMessage("\ninvalid input, please try again\n");
                            break;
                    }
                }
                else
                {
                    inOut.displayMessage("\ninvalid input, please try again\n");
                }
            }
        }

        private void removeNode(Node theNode)
        {
            if (theNode == null)
                return;

            //remove head
            if (theNode == _headFirst)
            {
                if (_headFirst.next == null)
                {
                    theNode = null;
                    _headFirst = null;
                    return;
                }
                _headFirst = _headFirst.next;
                _headFirst.prev = null;
                theNode = null;
                return;
            }

            //remove last node
            if (theNode.next == null)
            {
                theNode.prev.next = null;
                theNode = null;
                return;
            }

            //remove node from middle
            theNode.prev.next = theNode.next;
            theNode.next.prev = theNode.prev;
            theNode = null;
        }

        private String nameToFind(InputOutput inOut, String message)
        {
            return inOut.obtainDataFromUser(message);
        }

        private Node findNode(String name)
        {
            Node tempNode = new Node();
            tempNode.setFirstName(name);
            Node[] index = makeNodeArray();

            int beginningRange = findRange(index, tempNode);

            return findNodeInRange(name, index, beginningRange);
        }

        private Node findNodeInRange(string name, Node[] index, int beginningRange)
        {
            if (index[beginningRange].getFirstName().ToLower().Equals(name))
                return index[beginningRange];
            if (beginningRange + 1 == index.Length)
                return null;
            if (index[beginningRange + 1].getFirstName().ToLower().Equals(name))
                return index[beginningRange + 1];
            if (beginningRange + 2 == index.Length)
                return null;
            if (index[beginningRange + 2].getFirstName().ToLower().Equals(name))
                return index[beginningRange + 2];
            if (beginningRange + 3 == index.Length)
                return null;
            if (index[beginningRange + 3].getFirstName().ToLower().Equals(name))
                return index[beginningRange + 3];
            return null;//node isn't there
        }

        private String lookAtTheList()
        {
            String outputString = "\n";
            int count = -1;

            if (_headFirst == null)
                return "\nthere are no nodes on this list\n";

            Node temp = _headFirst;

            do
            {
                if (++count == 5)
                {
                    outputString += "\n";
                    count = 0;
                }

                outputString += temp.getFirstName() + "-" + temp.getLastName() + ",  ";

                temp = temp.next;
            } while (temp != null);


            return (outputString + "\n");
        }

        private String lookAtOneNode(Node theNode, String name)
        {
            return "\nfound the name " + name + " in the node with name = " + theNode.getFirstName() + "-" + theNode.getLastName() + "\n";
        }

        private void initializeNode(Node newNode, InputOutput inOut)
        {
            newNode.setFirstName(inOut.obtainDataFromUser("what it this node's first identifier?  "));
            newNode.setLastName(inOut.obtainDataFromUser("what it this node's last identifier? "));
        }

        private void initializeNode(Node newNode, String theFirstName, String theLastName)
        {
            newNode.setFirstName(theFirstName);
            newNode.setLastName(theLastName);
        }

        private void insertNode(Node newNode)
        {
            findInsertionPoint(newNode);
        }

        private void findInsertionPoint(Node newNode)
        {
            Node temp = _headFirst;

            //make array of Nodes to mirror number of nodes in linked list to aid searches
            Node[] index = makeNodeArray();

            //special case empty list
            if (_headFirst == null)
                _headFirst = newNode;

            //special case - newNode belongs before _headFirst
            else if (newNode.getFirstName().ToLower().CompareTo(_headFirst.getFirstName().ToLower()) < 0)
                insertNodeBefore_headFirst(newNode);

            //special case - newNode belongs after last node
            else if (newNode.getFirstName().ToLower().CompareTo(index[index.Length - 1].getFirstName().ToLower()) > 0)
                insertNodeAfterLastNode(newNode, index);

            else
                insertNodeInMiddle(newNode, index, findRange(index, newNode));
        }

        private void insertNodeInMiddle(Node newNode, Node[] index, int min)
        {
            if (newNode.getFirstName().ToLower().CompareTo(index[min].getFirstName().ToLower()) < 0)
                insertNodeInMiddle(newNode, index[min]);
            else if (newNode.getFirstName().ToLower().CompareTo(index[min + 1].getFirstName().ToLower()) < 0)
                insertNodeInMiddle(newNode, index[min + 1]);
            else if (newNode.getFirstName().ToLower().CompareTo(index[min + 2].getFirstName().ToLower()) < 0)
                insertNodeInMiddle(newNode, index[min + 2]);
            else if (newNode.getFirstName().ToLower().CompareTo(index[min + 3].getFirstName().ToLower()) < 0)
                insertNodeInMiddle(newNode, index[min + 3]);
        }

        private int findRange(Node[] index, Node newNode)
        {
            int min = 0;
            int max = index.Length - 1;
            int mid = (max - min) / 2;
            int span = max - min;

            while (span > 3)
            {
                //if mid is ip
                if (newNode.getFirstName().ToLower().Equals(index[mid].getFirstName().ToLower()))
                    return mid;

                if (newNode.getFirstName().ToLower().CompareTo(index[mid].getFirstName().ToLower()) < 0)
                    max = mid;//IP is in first half
                else
                    min = mid;//IP is in second half

                mid = min + (max - min) / 2;
                span = max - min;
            }

            return min;
        }

        private void insertNodeInMiddle(Node newNode, Node ip)
        {
            newNode.next = ip;
            newNode.prev = ip.prev;
            ip.prev = newNode;
            newNode.prev.next = newNode;
        }

        private void insertNodeAfterLastNode(Node newNode, Node[] index)
        {
            newNode.prev = index[index.Length - 1];
            index[index.Length - 1].next = newNode;
        }

        private void insertNodeBeforeMid(Node newNode, Node[] index, int mid)
        {
            newNode.prev = index[mid].prev;
            newNode.next = index[mid];
            index[mid].prev.next = newNode;
            index[mid].prev = newNode;
        }

        private void insertNodeBefore_headFirst(Node newNode)
        {
            _headFirst.prev = newNode;
            newNode.next = _headFirst;
            _headFirst = newNode;
        }

        private void insertNodeAtEnd(Node IP, Node newNode)
        {
            IP.next = newNode;
            newNode.prev = IP;
        }

        private Node createNewNode()
        {
            //instantiate Node
            return new Node();
        }

        private Node[] makeNodeArray()
        {
            if (_headFirst == null)
                return null;

            Node[] nodeLocations;
            int n = 1;

            //count the number of nodes in the list
            Node temp = _headFirst;
            while (temp.next != null)
            {
                n++;
                temp = temp.next;
            }

            nodeLocations = new Node[n];

            temp = _headFirst;
            for (n = 0; n < nodeLocations.Length; n++, temp = temp.next)
            {
                nodeLocations[n] = temp;
            }

            return nodeLocations;
        }

        private bool canBePositiveInt(String s)
        {
            try
            {
                if (Convert.ToInt16(s) > 0)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
