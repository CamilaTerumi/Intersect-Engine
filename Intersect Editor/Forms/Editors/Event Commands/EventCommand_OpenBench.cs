﻿using System;
using System.Windows.Forms;
using Intersect.Editor.Classes;
using Intersect.Enums;
using Intersect.GameObjects.Events;
using Intersect.Localization;

namespace Intersect.Editor.Forms.Editors.Event_Commands
{
    public partial class EventCommand_OpenBench : UserControl
    {
        private readonly FrmEvent _eventEditor;
        private EventCommand _myCommand;

        public EventCommand_OpenBench(EventCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            _myCommand = refCommand;
            _eventEditor = editor;
            InitLocalization();
            cmbbench.Items.Clear();
            cmbbench.Items.AddRange(Database.GetGameObjectList(GameObjectType.Bench));
            cmbbench.SelectedIndex = Database.GameObjectListIndex(GameObjectType.Bench, _myCommand.Ints[0]);
        }

        private void InitLocalization()
        {
            grpBench.Text = Strings.Get("eventopenbench", "title");
            lblBench.Text = Strings.Get("eventopenbench", "label");
            btnSave.Text = Strings.Get("eventopenbench", "okay");
            btnCancel.Text = Strings.Get("eventopenbench", "cancel");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbbench.SelectedIndex > -1)
                _myCommand.Ints[0] = Database.GameObjectIdFromList(GameObjectType.Bench, cmbbench.SelectedIndex);
            _eventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _eventEditor.CancelCommandEdit();
        }
    }
}