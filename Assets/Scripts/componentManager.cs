using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Xml;
using UnityEngine.UI;

public class componentManager : MonoBehaviour
{
    // Это тот самый сборник компонентов, поэтому прежде чем менять тут что-то ПОДУМАЙТЕ ТЩАТЕЛЬНО!!!
    
    public class movementComponent
    {
        protected GameObject controledObject;
        public movementComponent(GameObject objectToSet)
        {
            this.controledObject = objectToSet;
        }
        public void moveObject(float x, float y, float z)
        {
            controledObject.transform.position += controledObject.transform.forward * z * Time.deltaTime;
            controledObject.transform.position += controledObject.transform.right * x * Time.deltaTime;
            controledObject.GetComponent<Rigidbody>().AddForce(Vector3.up * y * Time.deltaTime);
        }
        public virtual void rotateObject(float rotateX, float rotateY, float rotateZ) {
            controledObject.transform.Rotate(Vector3.up * rotateY);
            controledObject.transform.Rotate(Vector3.forward * rotateZ);
            controledObject.transform.Rotate(Vector3.right * rotateX);
        }
    }

    public class limitedMovementComponent : movementComponent
    {

        public limitedMovementComponent(GameObject objectToSet) : base(objectToSet)
        {
        }
        public override void rotateObject(float rotateX, float rotateY, float rotateZ)
        {
            controledObject.transform.Rotate(Vector3.up * rotateY);
            controledObject.transform.Rotate(Vector3.forward * rotateZ);
            controledObject.transform.Rotate(Vector3.right * rotateX);

            Quaternion rot = controledObject.transform.localRotation;
            if (rot.x > 0.7f)
            {
                controledObject.transform.localEulerAngles = new Vector3(83f, rot.y, rot.z);
            }
            if (rot.x < -0.7f)
            {
                controledObject.transform.localEulerAngles = new Vector3(-83f, rot.y, rot.z);
            }
        }
    }

    public abstract class enemyNavigationComponent
    {
        protected GameObject target;
        protected NavMeshAgent agent;
        protected float lookRadius;
        public enemyNavigationComponent(GameObject targetToSet, NavMeshAgent agentToSet, float lookRadiusToSet)
        {
            this.target = targetToSet;
            this.agent = agentToSet;
            this.lookRadius = lookRadiusToSet;
        }

        public void moveEnemy()
        {
            float distance = Vector3.Distance(target.transform.position, agent.transform.position);
            if (distance <= lookRadius)
            {
                agent.SetDestination(target.transform.position);
                if (distance <= agent.stoppingDistance)
                {
                    this.attack();
                }
            }
        }
        protected virtual void attack()
        {

        }
    }

    public class shadowEnemyComponent : enemyNavigationComponent
    {
        public shadowEnemyComponent(GameObject targetToSet, NavMeshAgent agentToSet, float lookRadiusToSet) : base(targetToSet, agentToSet, lookRadiusToSet)
        {

        }
        protected override void attack()
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }

    public abstract class item
    {
        protected string name;
        protected Texture2D imageOfAnItem;
        public item(string nameToSet, string pathToImage)
        {
            this.name = nameToSet;
            Texture2D tex = new Texture2D(100, 100);
            this.imageOfAnItem = Resources.Load<Texture2D>(pathToImage);
            Debug.Log("Succes: ", Resources.Load(pathToImage));
        }

        public Texture getImage()
        {
            return this.imageOfAnItem;
        }

        public string getName()
        {
            return this.name;
        }
    }

    public class basicItem : item
    {
        public basicItem(string nameToSet, string pathToImage) : base(nameToSet, pathToImage)
        {

        }
    }

    public class inspectableItem: item
    {
        protected Mesh itemModel;
        public inspectableItem(string nameToSet, string pathToImage, string pathToMesh) : base(nameToSet, pathToImage)
        {
            this.itemModel= Resources.Load<Mesh>(pathToMesh);
        }
    }

    public class itemBuilder
    {
        public IDictionary<string, item> registeredItems = new Dictionary<string, item>();
        protected XmlDocument itemList = new XmlDocument();
        protected TextAsset itemListFile;
        public void registerItems()
        {
            itemListFile = (TextAsset)Resources.Load("itemList");
            itemList.LoadXml(itemListFile.text);
            XmlNodeList nodes= itemList.FirstChild.ChildNodes;
            for (int i =0; i<nodes.Count; i++)
            {
                registeredItems.Add(nodes[i].Attributes.GetNamedItem("itemName").Value, new basicItem(nodes[i].Attributes.GetNamedItem("itemName").Value, "itemSprites/"+nodes[i].Attributes.GetNamedItem("image").Value));
                Debug.Log("itemSprites/" + nodes[i].Attributes.GetNamedItem("image").Value);
            }
        }
    }
    public abstract class inventory
    {
        protected inventorySlot[] slots = {null,null,null,null,null,null};
        protected item[] items = { null, null, null, null, null, null };
        public inventory()
        {
            
        }
         
        public void setSlot(inventorySlot slot)
        {
            this.slots[slot.slotID] = slot;
        }

        public List<string> getInventoryItems()
        {
            List<string> toReturn = new List<string>();
            foreach (item itemToReturn in this.items)
            {
                if (itemToReturn != null){
                    toReturn.Add(itemToReturn.getName());
                }
                
            }
            return toReturn;
        }

        public bool isItemPresent(string itemToFind)
        {
            foreach (item itemToCheck in this.items)
            {
                if (itemToCheck != null)
                {
                    if (itemToCheck.getName() == itemToFind)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public void addItem(item itemToAdd)
        {
            for (int i=0;i<this.slots.Length; i++)
            {
                if (this.items[i] == null)
                {
                    this.slots[i].setItem(itemToAdd);
                    this.items[i] = itemToAdd;
                    break;
                }
                
            }
            
        }

    }

    public class basicInventory:inventory
    {
        

    }

    public class inventorySlot
    {
        public int slotID { get; }
        protected item slotItem=null;
        protected GameObject slotDisplay;
        protected GameObject slotButton;
        public item getItem()
        {
            return this.slotItem;
        }
        public inventorySlot(int IDtoSet, GameObject slotObject)
        {
            this.slotID = IDtoSet;
            this.slotDisplay = slotObject;
            this.slotButton = slotDisplay.transform.GetChild(0).gameObject;
            this.slotButton.GetComponent<Button>().onClick.AddListener(this.clearSlot);
        }

        public void setItem(item itemToSet)
        {
            this.slotItem = itemToSet;
            this.slotDisplay.GetComponent<RawImage>().texture = itemToSet.getImage();
            Debug.Log(itemToSet.getImage());
        }

        public void clearSlot()
        {
            Debug.Log("slot cleared!");
            this.slotItem = null;
            this.slotDisplay.GetComponent<RawImage>().texture = null;
        }
        public int getId() 
        {
            return this.slotID;
        }
    }

    void Update()
    {
        
    }
}
