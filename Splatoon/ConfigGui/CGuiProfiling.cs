﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Splatoon.Profiling;

namespace Splatoon
{
    partial class CGui
    {
        void DisplayProfiling()
        {
            ImGui.BeginChild("Profiling");
            ImGui.Checkbox("Enable profiler", ref p.Profiler.Enabled);
            ImGui.SameLine();
            if (ImGui.Button("Reset all"))
            {
                p.Profiler.MainTick.Reset();
                p.Profiler.MainTickActorTableScan.Reset();
                p.Profiler.MainTickCalcDynamic.Reset();
                p.Profiler.MainTickCalcPresets.Reset();
                p.Profiler.MainTickDequeue.Reset();
                p.Profiler.MainTickFind.Reset();
                p.Profiler.MainTickPrepare.Reset();
                p.Profiler.MainTickChat.Reset();
                p.Profiler.Gui.Reset();
                p.Profiler.GuiLines.Reset();
            }
            ImGui.Columns(4);
            ImGui.SetColumnWidth(0, ImGui.GetWindowContentRegionWidth() / 4);
            ImGui.SetColumnWidth(1, ImGui.GetWindowContentRegionWidth() / 4);
            ImGui.SetColumnWidth(2, ImGui.GetWindowContentRegionWidth() / 4);
            ImGui.SetColumnWidth(3, ImGui.GetWindowContentRegionWidth() / 4);
            DisplayProfiler("Main tick\nTotal", p.Profiler.MainTick);
            ImGui.NextColumn();
            DisplayProfiler("Main tick\nDequeue", p.Profiler.MainTickDequeue);
            ImGui.NextColumn();
            DisplayProfiler("Main tick\nPrepare tick", p.Profiler.MainTickPrepare);
            ImGui.NextColumn();
            DisplayProfiler("Main tick\nProcess splatoon find", p.Profiler.MainTickFind);
            ImGui.NextColumn();
            ImGui.Separator();
            DisplayProfiler("Main tick\nProcess user-defined layouts", p.Profiler.MainTickCalcPresets);
            ImGui.NextColumn();
            DisplayProfiler("Main tick\nProcess dynamic elements", p.Profiler.MainTickCalcDynamic);
            ImGui.NextColumn();
            DisplayProfiler("Main tick\nScan actor table", p.Profiler.MainTickActorTableScan);
            ImGui.NextColumn();
            DisplayProfiler("Main tick\nProcess chat message", p.Profiler.MainTickChat);
            ImGui.NextColumn();
            ImGui.Separator();
            ImGui.Columns(2);
            ImGui.SetColumnWidth(0, ImGui.GetWindowContentRegionWidth() / 2);
            ImGui.SetColumnWidth(1, ImGui.GetWindowContentRegionWidth() / 2);
            DisplayProfiler("GUI: total", p.Profiler.Gui);
            ImGui.NextColumn();
            DisplayProfiler("GUI: lines", p.Profiler.GuiLines);
            ImGui.Columns(1);
            ImGui.EndChild();
        }

        void DisplayProfiler(string name, StopwatchWrapper w)
        {
            ImGui.TextColored(Colors.Green.ToVector4(), name);
            ImGui.Text("Total time: " + w.GetTotalTime());
            ImGui.Text("Total ticks: " + w.GetTotalTicks());
            ImGui.Text("Ticks avg: " + w.GetAverageTicks().ToString("0.00"));
            ImGui.TextColored(Colors.Yellow.ToVector4(), "MS avg: " + w.GetAverageMSPT().ToString("0.0000") + " ms");
            if (ImGui.Button("Reset##SW" + name))
            {
                w.Reset();
            }
        }
    }
}
