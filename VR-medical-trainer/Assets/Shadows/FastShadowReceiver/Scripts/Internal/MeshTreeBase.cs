﻿//
// MeshTreeBase.cs
//
// Fast Shadow Receiver
//
// Copyright 2014 NYAHOON GAMES PTE. LTD. All Rights Reserved.
//
using UnityEngine;
using System.Threading;

namespace FastShadowReceiver {
	public abstract class MeshTreeBase : ScriptableObject {
		[SerializeField][HideInInspector]
		protected Bounds m_bounds;
		public Bounds bounds
		{
			get { return m_bounds; }
		}

		private ManualResetEvent m_event;

		public abstract bool IsPrebuilt();
		public abstract void BuildFromPrebuiltData();
		public abstract bool IsReadyToBuild();
		public abstract bool IsBuildFinished();
		public abstract string CheckError(GameObject rootObject);
#if UNITY_EDITOR
		public abstract int GetMemoryUsage();
		public abstract int GetNodeCount();
		public abstract float GetBuildProgress();
#endif
		public bool IsBuilding()
		{
			return (!IsBuildFinished() && m_event != null);
		}
		public void AsyncBuild()
		{
			if (!IsBuildFinished() && m_event != null) {
				// already start building
				return;
			}
			if (m_event == null) {
				m_event = new ManualResetEvent(false);
			}
			m_event.Reset();
			PrepareForBuild();
			Nyahoon.ThreadPool.InitInstance();
			Nyahoon.ThreadPool.QueueUserWorkItem(arg => ((MeshTreeBase)arg).BuildStart(), this);
		}
		public void WaitForBuild()
		{
			if (m_event != null) {
				m_event.WaitOne();
				m_event = null;
			}
		}
		public abstract System.Type GetSearchType();
		public abstract MeshTreeSearch CreateSearch();
		public abstract void Search(MeshTreeSearch search);
		public abstract void Raycast(MeshTreeRaycast raycast);

		private void BuildStart()
		{
			try {
				Build();
			}
			catch (System.Exception e) {
				Debug.LogException(e, this);
			}
			finally {
				m_event.Set();
			}
		}
		protected abstract void PrepareForBuild();
		protected abstract void Build();
	}
}
