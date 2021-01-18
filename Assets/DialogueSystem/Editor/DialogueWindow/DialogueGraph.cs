using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace DialogueSystem.Editor
{
    public class DialogueGraph : EditorWindow
    {
        private DialogueGraphView graphview;
        private SaveLoadUtils svUtil;

        private void Awake()
        {
            svUtil = new SaveLoadUtils();
        }

        [MenuItem("Graph/Dialogue Graph")]
        public static void OpenDialogueGraphWindow()
        {
            DialogueGraph window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent(text: "Dialogue Graph");

        }

        public static void OpenDialogueGraphWindow(DialogueScript ds)
        {
            DialogueGraph window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent(text: ds.DialogueName);
            SaveLoadUtils svUtil = new SaveLoadUtils();
            svUtil.LoadDialogues(window.graphview, ds);
            
        }

        private void OnEnable()
        {
            CreateGraphView();
            CreateToolbar();
        }

        private void CreateToolbar()
        {
            Toolbar toolbar = new Toolbar();

            Button nodeCreateButton = new Button(clickEvent: () =>
            {
                graphview.CreateDialogueNode();
            });

            Button saveButton = new Button(clickEvent: () =>
            {

                //Open popUp window

                svUtil.SaveDialogues(graphview, graphview.DialogueName);
            });

            nodeCreateButton.text = "Create Node";
            saveButton.text = "Save Dialogue";


            toolbar.Add(nodeCreateButton);
            toolbar.Add(saveButton);


            rootVisualElement.Add(toolbar);
        }

        private void CreateGraphView()
        {
            graphview = new DialogueGraphView
            {
                name = "Dialogue Graph"
            };

            graphview.StretchToParentSize();
            rootVisualElement.Add(graphview);
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(graphview);
        }

    }
}
