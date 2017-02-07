using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class BackLog : MonoBehaviour {

	public int logMax = 30;
	public GameObject originalElement;

	List<Element> elementList;
	Stack<Element> emptyElementStack;


	// Use this for initialization
	void Start () {
		elementList = new List<Element>(logMax);
		emptyElementStack = new Stack<Element>(logMax);
		for (int i=0; i<logMax; i++) {
			var element = new Element();
			element.gameObject = GameObject.Instantiate(originalElement, originalElement.transform.parent, false);
			element.gameObject.SetActive(false);
			element.text = element.gameObject.GetComponentInChildren<Text>();
			emptyElementStack.Push(element);
		}
		originalElement.SetActive(false);

		InitializeLayout();

	}

	class Element {
		public GameObject gameObject;
		public Text text;
	}

	public void Log(string message)
	{
		Element element;
		if (0 < emptyElementStack.Count) {
			element = emptyElementStack.Pop();
		}
		else {
			element = elementList[0];
			elementList.RemoveAt(0);
		}
		element.gameObject.SetActive(true);
		element.gameObject.transform.SetParent(null,false);
		element.gameObject.transform.SetParent(originalElement.transform.parent,false);
		element.text.text = message;
		elementList.Add(element);

		LayoutOnAddElement();
	}


	public enum Alignment {
		Upper,
		Lower,
	}
	public Alignment alignment;
	ScrollRect scrollRect;

	void InitializeLayout()
	{
		var rectTransform = GetComponent<RectTransform>();
		rectTransform.pivot = new Vector2(0, (alignment == Alignment.Upper) ? 1:0);
		scrollRect = GetComponentInParent<ScrollRect>();

	}
	void LayoutOnAddElement()
	{
		StartCoroutine(LayoutOnAddElementAsync());
	}
	IEnumerator LayoutOnAddElementAsync()
	{
		yield return null;
		scrollRect.verticalNormalizedPosition = 0;
	}

}
