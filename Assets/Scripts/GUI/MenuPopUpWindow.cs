﻿// Copyright 2020 The Tilt Brush Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.IO;
using UnityEngine;
namespace TiltBrush
{

    class MenuPopUpWindow : PopUpWindow
    {
        public override void SetPopupCommandParameters(int iCommandParam, int iCommandParam2)
        {
            // TODO : Fix this hangnail.
            OptionButton[] optionButtons = GetComponentsInChildren<OptionButton>();
            foreach (OptionButton button in optionButtons)
            {
                button.SetCommandParameters(iCommandParam, iCommandParam2);
            }

            // The rename button should only be enabled for categories that support renaming
            var renameButton = GetComponentInChildren<KeyboardPopupButton>();
            SketchSetType sketchSetType = (SketchSetType)iCommandParam2;
            renameButton.SetButtonAvailable(sketchSetType == SketchSetType.User);
        }

        // This code is specific to the "Rename" button in the Sketchbook menu
        // This popup class is currently only used for the Sketchbook menu
        // If that changes then this probably belongs in a subclass
        public void SetInitialKeyboardText(KeyboardPopupButton btn)
        {
            SketchSetType sketchSetType = (SketchSetType)btn.m_CommandParam2;
            var sketchSet = SketchCatalog.m_Instance.GetSet(SketchSetType.User) as FileSketchSet;
            var sceneFileInfo = sketchSet.GetSketchSceneFileInfo(btn.m_CommandParam);
            var currentName = Path.GetFileName(sceneFileInfo.FullPath);
            if (currentName.EndsWith(SaveLoadScript.TILT_SUFFIX))
            {
                currentName = currentName.Substring(0, currentName.Length - SaveLoadScript.TILT_SUFFIX.Length);
            }
            KeyboardPopUpWindow.m_InitialText = currentName;
        }
    }

} // namespace TiltBrush
