namespace RageKit.GameFiles.FileTypes
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml;

    using SharpDX;

    using EXP = System.ComponentModel.ExpandableObjectConverter;
    using TC = System.ComponentModel.TypeConverterAttribute;

    public class VehicleHandlingFile : GameFile, PackedFile
    {
        public List<VehicleHandlingData> Handlings { get; set; }

        public VehicleHandlingFile()
            : base(null, GameFileType.VehicleHandling)
        {
        }

        public VehicleHandlingFile(RpfFileEntry entry)
            : base(entry, GameFileType.VehicleHandling)
        {
        }

        public void Load(byte[] data, RpfFileEntry entry)
        {
            RpfFileEntry = entry;
            Name = entry.Name;
            FilePath = Name;

            if (entry.NameLower.EndsWith(".meta"))
            {
                var xml = TextUtil.GetUTF8Text(data);
                var doc = new XmlDocument();

                doc.LoadXml(xml);

                LoadHandlings(doc);

                Loaded = true;
            }
        }

        private void LoadHandlings(XmlDocument doc)
        {
            var nodes = doc.SelectNodes("CHandlingDataMgr/HandlingData/Item | CHandlingDataMgr/HandlingData/item");

            Handlings = new List<VehicleHandlingData>();

            foreach (XmlNode inode in nodes)
            {
                var item = new VehicleHandlingData();

                item.Load(inode);

                Handlings.Add(item);
            }
        }
    }

    public abstract class SubHandlingData
    {
        public abstract string type { get; }

        public abstract void Load(XmlNode node);

        public T As<T>()
            where T : SubHandlingData
        {
            return this is T ? this as T : null;
        }

        protected string[] GetStringItemArray(XmlNode node, string childName)
        {
            var cnode = node.SelectSingleNode(childName);
            var items = cnode?.SelectNodes("Item");

            if (items == null) { return null; }

            var tempArray = (from XmlNode inode in items select string.IsNullOrEmpty(inode.InnerText) ? string.Empty : inode.InnerText).ToList();

            return tempArray.Count == 0 ? null : tempArray.ToArray();
        }

        protected int[] GetIntArray(XmlNode node, string childName, char delimiter)
        {
            var tempArray = new List<int>();
            var ldastr = Xml.GetChildInnerText(node, childName);
            var ldarr = ldastr?.Split(delimiter);

            if (ldarr == null) { return null; }

            foreach (var ldstr in ldarr)
            {
                var ldt = ldstr.Trim();

                if (!string.IsNullOrEmpty(ldt) && int.TryParse(ldt, NumberStyles.Any, CultureInfo.InvariantCulture, out var i))
                {
                    tempArray.Add(i);
                }
            }

            return tempArray.Count == 0 ? null : tempArray.ToArray();
        }

        protected float[] GetFloatArray(XmlNode node, string childName, char delimiter)
        {
            var tempArray = new List<float>();
            var ldastr = Xml.GetChildInnerText(node, childName);
            var ldarr = ldastr?.Split(delimiter);

            if (ldarr == null) { return null; }

            foreach (var ldstr in ldarr)
            {
                var ldt = ldstr.Trim();

                if (!string.IsNullOrEmpty(ldt) && float.TryParse(ldt, NumberStyles.Any, CultureInfo.InvariantCulture, out var i))
                {
                    tempArray.Add(i);
                }
            }

            return tempArray.Count == 0 ? null : tempArray.ToArray();
        }
    }

    [TC(typeof(EXP))]
    public class CBoatHandlingData : SubHandlingData
    {
        public override string type => nameof(CBoatHandlingData);
        public float fBoxFrontMult { get; set; }
        public float fBoxRearMult { get; set; }
        public float fBoxSideMult { get; set; }
        public float fSampleTop { get; set; }
        public float fSampleBottom { get; set; }
        public float fAquaplaneForce { get; set; }
        public float fAquaplanePushWaterMult { get; set; }
        public float fAquaplanePushWaterCap { get; set; }
        public float fAquaplanePushWaterApply { get; set; }
        public float fRudderForce { get; set; }
        public float fRudderOffsetSubmerge { get; set; }
        public float fRudderOffsetForce { get; set; }
        public float fRudderOffsetForceZMult { get; set; }
        public float fWaveAudioMult { get; set; }
        public Vector3 vecMoveResistance { get; set; }
        public Vector3 vecTurnResistance { get; set; }
        public float fLook_L_R_CamHeight { get; set; }
        public float fDragCoefficient { get; set; }
        public float fKeelSphereSize { get; set; }
        public float fPropRadius { get; set; }
        public float fLowLodAngOffset { get; set; }
        public float fLowLodDraughtOffset { get; set; }
        public float fImpellerOffset { get; set; }
        public float fImpellerForceMult { get; set; }
        public float fDinghySphereBuoyConst { get; set; }
        public float fProwRaiseMult { get; set; }
        public float fDeepWaterSampleBuoyancyMult { get; set; }

        public override void Load(XmlNode node)
        {
            fBoxFrontMult = Xml.GetChildFloatAttribute(node, nameof(fBoxFrontMult));
            fBoxRearMult = Xml.GetChildFloatAttribute(node, nameof(fBoxRearMult));
            fBoxSideMult = Xml.GetChildFloatAttribute(node, nameof(fBoxSideMult));
            fSampleTop = Xml.GetChildFloatAttribute(node, nameof(fSampleTop));
            fSampleBottom = Xml.GetChildFloatAttribute(node, nameof(fSampleBottom));
            fAquaplaneForce = Xml.GetChildFloatAttribute(node, nameof(fAquaplaneForce));
            fAquaplanePushWaterMult = Xml.GetChildFloatAttribute(node, nameof(fAquaplanePushWaterMult));
            fAquaplanePushWaterCap = Xml.GetChildFloatAttribute(node, nameof(fAquaplanePushWaterCap));
            fAquaplanePushWaterApply = Xml.GetChildFloatAttribute(node, nameof(fAquaplanePushWaterApply));
            fRudderForce = Xml.GetChildFloatAttribute(node, nameof(fRudderForce));
            fRudderOffsetSubmerge = Xml.GetChildFloatAttribute(node, nameof(fRudderOffsetSubmerge));
            fRudderOffsetForce = Xml.GetChildFloatAttribute(node, nameof(fRudderOffsetForce));
            fRudderOffsetForceZMult = Xml.GetChildFloatAttribute(node, nameof(fRudderOffsetForceZMult));
            fWaveAudioMult = Xml.GetChildFloatAttribute(node, nameof(fWaveAudioMult));
            vecMoveResistance = Xml.GetChildVector3Attributes(node, nameof(vecMoveResistance));
            vecTurnResistance = Xml.GetChildVector3Attributes(node, nameof(vecTurnResistance));
            fLook_L_R_CamHeight = Xml.GetChildFloatAttribute(node, nameof(fLook_L_R_CamHeight));
            fDragCoefficient = Xml.GetChildFloatAttribute(node, nameof(fDragCoefficient));
            fKeelSphereSize = Xml.GetChildFloatAttribute(node, nameof(fKeelSphereSize));
            fPropRadius = Xml.GetChildFloatAttribute(node, nameof(fPropRadius));
            fLowLodAngOffset = Xml.GetChildFloatAttribute(node, nameof(fLowLodAngOffset));
            fLowLodDraughtOffset = Xml.GetChildFloatAttribute(node, nameof(fLowLodDraughtOffset));
            fImpellerOffset = Xml.GetChildFloatAttribute(node, nameof(fImpellerOffset));
            fImpellerForceMult = Xml.GetChildFloatAttribute(node, nameof(fImpellerForceMult));
            fDinghySphereBuoyConst = Xml.GetChildFloatAttribute(node, nameof(fDinghySphereBuoyConst));
            fProwRaiseMult = Xml.GetChildFloatAttribute(node, nameof(fProwRaiseMult));
            fDeepWaterSampleBuoyancyMult = Xml.GetChildFloatAttribute(node, nameof(fDeepWaterSampleBuoyancyMult));
        }
    }

    [TC(typeof(EXP))]
    public class CBikeHandlingData : SubHandlingData
    {
        public override string type => nameof(CBikeHandlingData);
        public float fLeanFwdCOMMult { get; set; }
        public float fLeanFwdForceMult { get; set; }
        public float fLeanBakCOMMult { get; set; }
        public float fLeanBakForceMult { get; set; }
        public float fMaxBankAngle { get; set; }
        public float fFullAnimAngle { get; set; }
        public float fDesLeanReturnFrac { get; set; }
        public float fStickLeanMult { get; set; }
        public float fBrakingStabilityMult { get; set; }
        public float fInAirSteerMult { get; set; }
        public float fWheelieBalancePoint { get; set; }
        public float fStoppieBalancePoint { get; set; }
        public float fWheelieSteerMult { get; set; }
        public float fRearBalanceMult { get; set; }
        public float fFrontBalanceMult { get; set; }
        public float fBikeGroundSideFrictionMult { get; set; }
        public float fBikeWheelGroundSideFrictionMult { get; set; }
        public float fBikeOnStandLeanAngle { get; set; }
        public float fBikeOnStandSteerAngle { get; set; }
        public float fJumpForce { get; set; }

        public override void Load(XmlNode node)
        {
            fLeanFwdCOMMult = Xml.GetChildFloatAttribute(node, nameof(fLeanFwdCOMMult));
            fLeanFwdForceMult = Xml.GetChildFloatAttribute(node, nameof(fLeanFwdForceMult));
            fLeanBakCOMMult = Xml.GetChildFloatAttribute(node, nameof(fLeanBakCOMMult));
            fLeanBakForceMult = Xml.GetChildFloatAttribute(node, nameof(fLeanBakForceMult));
            fMaxBankAngle = Xml.GetChildFloatAttribute(node, nameof(fMaxBankAngle));
            fFullAnimAngle = Xml.GetChildFloatAttribute(node, nameof(fFullAnimAngle));
            fDesLeanReturnFrac = Xml.GetChildFloatAttribute(node, nameof(fDesLeanReturnFrac));
            fStickLeanMult = Xml.GetChildFloatAttribute(node, nameof(fStickLeanMult));
            fBrakingStabilityMult = Xml.GetChildFloatAttribute(node, nameof(fBrakingStabilityMult));
            fInAirSteerMult = Xml.GetChildFloatAttribute(node, nameof(fInAirSteerMult));
            fWheelieBalancePoint = Xml.GetChildFloatAttribute(node, nameof(fWheelieBalancePoint));
            fStoppieBalancePoint = Xml.GetChildFloatAttribute(node, nameof(fStoppieBalancePoint));
            fWheelieSteerMult = Xml.GetChildFloatAttribute(node, nameof(fWheelieSteerMult));
            fRearBalanceMult = Xml.GetChildFloatAttribute(node, nameof(fRearBalanceMult));
            fFrontBalanceMult = Xml.GetChildFloatAttribute(node, nameof(fFrontBalanceMult));
            fBikeGroundSideFrictionMult = Xml.GetChildFloatAttribute(node, nameof(fBikeGroundSideFrictionMult));
            fBikeWheelGroundSideFrictionMult = Xml.GetChildFloatAttribute(node, nameof(fBikeWheelGroundSideFrictionMult));
            fBikeOnStandLeanAngle = Xml.GetChildFloatAttribute(node, nameof(fBikeOnStandLeanAngle));
            fBikeOnStandSteerAngle = Xml.GetChildFloatAttribute(node, nameof(fBikeOnStandSteerAngle));
            fJumpForce = Xml.GetChildFloatAttribute(node, nameof(fJumpForce));
        }
    }

    [TC(typeof(EXP))]
    public class CFlyingHandlingData : SubHandlingData
    {
        public override string type => nameof(CFlyingHandlingData);
        public float fThrust { get; set; }
        public float fThrustFallOff { get; set; }
        public float fThrustVectoring { get; set; }
        public float fYawMult { get; set; }
        public float fYawStabilise { get; set; }
        public float fSideSlipMult { get; set; }
        public float fRollMult { get; set; }
        public float fRollStabilise { get; set; }
        public float fPitchMult { get; set; }
        public float fPitchStabilise { get; set; }
        public float fFormLiftMult { get; set; }
        public float fAttackLiftMult { get; set; }
        public float fAttackDiveMult { get; set; }
        public float fGearDownDragV { get; set; }
        public float fGearDownLiftMult { get; set; }
        public float fWindMult { get; set; }
        public float fMoveRes { get; set; }
        public Vector3 vecTurnRes { get; set; }
        public Vector3 vecSpeedRes { get; set; }
        public float fGearDoorFrontOpen { get; set; }
        public float fGearDoorRearOpen { get; set; }
        public float fGearDoorRearOpen2 { get; set; }
        public float fGearDoorRearMOpen { get; set; }
        public float fTurublenceMagnitudeMax { get; set; }
        public float fTurublenceForceMulti { get; set; }
        public float fTurublenceRollTorqueMulti { get; set; }
        public float fTurublencePitchTorqueMulti { get; set; }
        public float fBodyDamageControlEffectMult { get; set; }
        public float fInputSensitivityForDifficulty { get; set; }
        public float fOnGroundYawBoostSpeedPeak { get; set; }
        public float fOnGroundYawBoostSpeedCap { get; set; }
        public float fEngineOffGlideMulti { get; set; }
        public float fSubmergeLevelToPullHeliUnderwater { get; set; }
        public string handlingType { get; set; }

        public override void Load(XmlNode node)
        {
            fThrust = Xml.GetChildFloatAttribute(node, nameof(fThrust));
            fThrustFallOff = Xml.GetChildFloatAttribute(node, nameof(fThrustFallOff));
            fThrustVectoring = Xml.GetChildFloatAttribute(node, nameof(fThrustVectoring));
            fYawMult = Xml.GetChildFloatAttribute(node, nameof(fYawMult));
            fYawStabilise = Xml.GetChildFloatAttribute(node, nameof(fYawStabilise));
            fSideSlipMult = Xml.GetChildFloatAttribute(node, nameof(fSideSlipMult));
            fRollMult = Xml.GetChildFloatAttribute(node, nameof(fRollMult));
            fRollStabilise = Xml.GetChildFloatAttribute(node, nameof(fRollStabilise));
            fPitchMult = Xml.GetChildFloatAttribute(node, nameof(fPitchMult));
            fPitchStabilise = Xml.GetChildFloatAttribute(node, nameof(fPitchStabilise));
            fFormLiftMult = Xml.GetChildFloatAttribute(node, nameof(fFormLiftMult));
            fAttackLiftMult = Xml.GetChildFloatAttribute(node, nameof(fAttackLiftMult));
            fAttackDiveMult = Xml.GetChildFloatAttribute(node, nameof(fAttackDiveMult));
            fGearDownDragV = Xml.GetChildFloatAttribute(node, nameof(fGearDownDragV));
            fGearDownLiftMult = Xml.GetChildFloatAttribute(node, nameof(fGearDownLiftMult));
            fWindMult = Xml.GetChildFloatAttribute(node, nameof(fWindMult));
            fMoveRes = Xml.GetChildFloatAttribute(node, nameof(fMoveRes));
            vecTurnRes = Xml.GetChildVector3Attributes(node, nameof(vecTurnRes));
            vecSpeedRes = Xml.GetChildVector3Attributes(node, nameof(vecSpeedRes));
            fGearDoorFrontOpen = Xml.GetChildFloatAttribute(node, nameof(fGearDoorFrontOpen));
            fGearDoorRearOpen = Xml.GetChildFloatAttribute(node, nameof(fGearDoorRearOpen));
            fGearDoorRearOpen2 = Xml.GetChildFloatAttribute(node, nameof(fGearDoorRearOpen2));
            fGearDoorRearMOpen = Xml.GetChildFloatAttribute(node, nameof(fGearDoorRearMOpen));
            fTurublenceMagnitudeMax = Xml.GetChildFloatAttribute(node, nameof(fTurublenceMagnitudeMax));
            fTurublenceForceMulti = Xml.GetChildFloatAttribute(node, nameof(fTurublenceForceMulti));
            fTurublenceRollTorqueMulti = Xml.GetChildFloatAttribute(node, nameof(fTurublenceRollTorqueMulti));
            fTurublencePitchTorqueMulti = Xml.GetChildFloatAttribute(node, nameof(fTurublencePitchTorqueMulti));
            fBodyDamageControlEffectMult = Xml.GetChildFloatAttribute(node, nameof(fBodyDamageControlEffectMult));
            fInputSensitivityForDifficulty = Xml.GetChildFloatAttribute(node, nameof(fInputSensitivityForDifficulty));
            fOnGroundYawBoostSpeedPeak = Xml.GetChildFloatAttribute(node, nameof(fOnGroundYawBoostSpeedPeak));
            fOnGroundYawBoostSpeedCap = Xml.GetChildFloatAttribute(node, nameof(fOnGroundYawBoostSpeedCap));
            fEngineOffGlideMulti = Xml.GetChildFloatAttribute(node, nameof(fEngineOffGlideMulti));
            fSubmergeLevelToPullHeliUnderwater = Xml.GetChildFloatAttribute(node, nameof(fSubmergeLevelToPullHeliUnderwater));
            handlingType = Xml.GetChildInnerText(node, nameof(handlingType));
        }
    }

    [TC(typeof(EXP))]
    public class CVehicleWeaponHandlingData : SubHandlingData
    {
        public override string type => nameof(CVehicleWeaponHandlingData);
        public string[] uWeaponHash { get; set; }
        public int[] WeaponSeats { get; set; }
        public string[] WeaponVehicleModType { get; set; }
        public float[] fTurretSpeed { get; set; }
        public float[] fTurretPitchMin { get; set; }
        public float[] fTurretPitchMax { get; set; }
        public float[] fTurretCamPitchMin { get; set; }
        public float[] fTurretCamPitchMax { get; set; }
        public float[] fBulletVelocityForGravity { get; set; }
        public float[] fTurretPitchForwardMin { get; set; }
        public float fUvAnimationMult { get; set; }
        public float fMiscGadgetVar { get; set; }
        public float fWheelImpactOffset { get; set; }

        public override void Load(XmlNode node)
        {
            uWeaponHash = GetStringItemArray(node, nameof(uWeaponHash));
            WeaponSeats = GetIntArray(node, nameof(WeaponSeats), '\n');
            WeaponVehicleModType = GetStringItemArray(node, nameof(WeaponVehicleModType));
            fTurretSpeed = GetFloatArray(node, nameof(fTurretSpeed), '\n');
            fTurretPitchMin = GetFloatArray(node, nameof(fTurretPitchMin), '\n');
            fTurretPitchMax = GetFloatArray(node, nameof(fTurretPitchMax), '\n');
            fTurretCamPitchMin = GetFloatArray(node, nameof(fTurretCamPitchMin), '\n');
            fTurretCamPitchMax = GetFloatArray(node, nameof(fTurretCamPitchMax), '\n');
            fBulletVelocityForGravity = GetFloatArray(node, nameof(fBulletVelocityForGravity), '\n');
            fTurretPitchForwardMin = GetFloatArray(node, nameof(fTurretPitchForwardMin), '\n');
            fUvAnimationMult = Xml.GetChildFloatAttribute(node, nameof(fUvAnimationMult));
            fMiscGadgetVar = Xml.GetChildFloatAttribute(node, nameof(fMiscGadgetVar));
            fWheelImpactOffset = Xml.GetChildFloatAttribute(node, nameof(fWheelImpactOffset));
        }
    }

    [TC(typeof(EXP))]
    public class CSubmarineHandlingData : SubHandlingData
    {
        public override string type => nameof(CSubmarineHandlingData);
        public float fPitchMult { get; set; }
        public float fPitchAngle { get; set; }
        public float fYawMult { get; set; }
        public float fDiveSpeed { get; set; }
        public float fRollMult { get; set; }
        public float fRollStab { get; set; }
        public Vector3 vTurnRes { get; set; }
        public float fMoveResXY { get; set; }
        public float fMoveResZ { get; set; }

        public override void Load(XmlNode node)
        {
            fPitchMult = Xml.GetChildFloatAttribute(node, nameof(fPitchMult));
            fPitchAngle = Xml.GetChildFloatAttribute(node, nameof(fPitchAngle));
            fYawMult = Xml.GetChildFloatAttribute(node, nameof(fYawMult));
            fDiveSpeed = Xml.GetChildFloatAttribute(node, nameof(fDiveSpeed));
            fRollMult = Xml.GetChildFloatAttribute(node, nameof(fRollMult));
            fRollStab = Xml.GetChildFloatAttribute(node, nameof(fRollStab));
            vTurnRes = Xml.GetChildVector3Attributes(node, nameof(vTurnRes));
            fMoveResXY = Xml.GetChildFloatAttribute(node, nameof(fMoveResXY));
            fMoveResZ = Xml.GetChildFloatAttribute(node, nameof(fMoveResZ));
        }
    }

    [TC(typeof(EXP))]
    public class CTrailerHandlingData : SubHandlingData
    {
        public override string type => nameof(CTrailerHandlingData);
        public float fAttachLimitPitch { get; set; }
        public float fAttachLimitRoll { get; set; }
        public float fAttachLimitYaw { get; set; }
        public float fUprightSpringConstant { get; set; }
        public float fUprightDampingConstant { get; set; }
        public float fAttachedMaxDistance { get; set; }
        public float fAttachedMaxPenetration { get; set; }
        public float fAttachRaiseZ { get; set; }
        public float fPosConstraintMassRatio { get; set; }

        public override void Load(XmlNode node)
        {
            fAttachLimitPitch = Xml.GetChildFloatAttribute(node, nameof(fAttachLimitPitch));
            fAttachLimitRoll = Xml.GetChildFloatAttribute(node, nameof(fAttachLimitRoll));
            fAttachLimitYaw = Xml.GetChildFloatAttribute(node, nameof(fAttachLimitYaw));
            fUprightSpringConstant = Xml.GetChildFloatAttribute(node, nameof(fUprightSpringConstant));
            fUprightDampingConstant = Xml.GetChildFloatAttribute(node, nameof(fUprightDampingConstant));
            fAttachedMaxDistance = Xml.GetChildFloatAttribute(node, nameof(fAttachedMaxDistance));
            fAttachedMaxPenetration = Xml.GetChildFloatAttribute(node, nameof(fAttachedMaxPenetration));
            fAttachRaiseZ = Xml.GetChildFloatAttribute(node, nameof(fAttachRaiseZ));
            fPosConstraintMassRatio = Xml.GetChildFloatAttribute(node, nameof(fPosConstraintMassRatio));
        }
    }

    [TC(typeof(EXP))]
    public class CAdvancedData : SubHandlingData
    {
        public override string type => nameof(CAdvancedData);
        public int Slot { get; set; }
        public int Index { get; set; }
        public float Value { get; set; }

        public override void Load(XmlNode node)
        {
            Slot = Xml.GetChildIntAttribute(node, nameof(Slot));
            Index = Xml.GetChildIntAttribute(node, nameof(Index));
            Value = Xml.GetChildFloatAttribute(node, nameof(Value));
        }
    }

    [TC(typeof(EXP))]
    public class CCarHandlingData : SubHandlingData
    {
        public override string type => nameof(CCarHandlingData);
        public float fBackEndPopUpCarImpulseMult { get; set; }
        public float fBackEndPopUpBuildingImpulseMult { get; set; }
        public float fBackEndPopUpMaxDeltaSpeed { get; set; }
        public float fToeFront { get; set; }
        public float fToeRear { get; set; }
        public float fCamberFront { get; set; }
        public float fCamberRear { get; set; }
        public float fCastor { get; set; }
        public float fEngineResistance { get; set; }
        public float fMaxDriveBiasTransfer { get; set; }
        public float fJumpForceScale { get; set; }
        public string strAdvancedFlags { get; set; }
        public CAdvancedData[] AdvancedData { get; set; }

        public override void Load(XmlNode node)
        {
            fBackEndPopUpCarImpulseMult = Xml.GetChildFloatAttribute(node, nameof(fBackEndPopUpCarImpulseMult));
            fBackEndPopUpBuildingImpulseMult = Xml.GetChildFloatAttribute(node, nameof(fBackEndPopUpBuildingImpulseMult));
            fBackEndPopUpMaxDeltaSpeed = Xml.GetChildFloatAttribute(node, nameof(fBackEndPopUpMaxDeltaSpeed));
            fCamberFront = Xml.GetChildFloatAttribute(node, nameof(fCamberFront));
            fCastor = Xml.GetChildFloatAttribute(node, nameof(fCastor));
            fToeFront = Xml.GetChildFloatAttribute(node, nameof(fToeFront));
            fCamberRear = Xml.GetChildFloatAttribute(node, nameof(fCamberRear));
            fToeRear = Xml.GetChildFloatAttribute(node, nameof(fToeRear));
            fEngineResistance = Xml.GetChildFloatAttribute(node, nameof(fEngineResistance));
            fMaxDriveBiasTransfer = Xml.GetChildFloatAttribute(node, nameof(fMaxDriveBiasTransfer));
            fJumpForceScale = Xml.GetChildFloatAttribute(node, nameof(fJumpForceScale));
            strAdvancedFlags = Xml.GetChildInnerText(node, nameof(strAdvancedFlags));
            AdvancedData = GetCAdvancedDataArray(node, nameof(AdvancedData));
        }

        private static CAdvancedData[] GetCAdvancedDataArray(XmlNode node, string childName)
        {
            var tempArray = new List<CAdvancedData>();
            var cnode = node.SelectSingleNode(childName);
            var items = cnode?.SelectNodes("Item");

            if (items == null) { return null; }

            foreach (XmlNode inote in items)
            {
                var item = new CAdvancedData();

                item.Load(inote);

                tempArray.Add(item);
            }

            return tempArray.Count == 0 ? null : tempArray.ToArray();
        }
    }

    [TC(typeof(EXP))]
    public class CBaseSubHandlingData : SubHandlingData
    {
        public override string type => nameof(CBaseSubHandlingData);

        public override void Load(XmlNode node)
        {
        }
    }

    [TC(typeof(EXP))]
    public class CSeaPlaneHandlingData : SubHandlingData
    {
        public override string type => nameof(CBoatHandlingData);
        public int fLeftPontoonComponentId { get; set; }
        public int fRightPontoonComponentId { get; set; }
        public float fPontoonBuoyConst { get; set; }
        public float fPontoonSampleSizeFront { get; set; }
        public float fPontoonSampleSizeMiddle { get; set; }
        public float fPontoonSampleSizeRear { get; set; }
        public float fPontoonLengthFractionForSamples { get; set; }
        public float fPontoonDragCoefficient { get; set; }
        public float fPontoonVerticalDampingCoefficientUp { get; set; }
        public float fPontoonVerticalDampingCoefficientDown { get; set; }
        public float fKeelSphereSize { get; set; }

        public override void Load(XmlNode node)
        {
            fLeftPontoonComponentId = Xml.GetChildIntInnerText(node, nameof(fLeftPontoonComponentId));
            fRightPontoonComponentId = Xml.GetChildIntInnerText(node, nameof(fRightPontoonComponentId));
            fPontoonBuoyConst = Xml.GetChildFloatAttribute(node, nameof(fPontoonBuoyConst));
            fPontoonSampleSizeFront = Xml.GetChildFloatAttribute(node, nameof(fPontoonSampleSizeFront));
            fPontoonSampleSizeMiddle = Xml.GetChildFloatAttribute(node, nameof(fPontoonSampleSizeMiddle));
            fPontoonSampleSizeRear = Xml.GetChildFloatAttribute(node, nameof(fPontoonSampleSizeRear));
            fPontoonLengthFractionForSamples = Xml.GetChildFloatAttribute(node, nameof(fPontoonLengthFractionForSamples));
            fPontoonDragCoefficient = Xml.GetChildFloatAttribute(node, nameof(fPontoonDragCoefficient));
            fPontoonVerticalDampingCoefficientUp = Xml.GetChildFloatAttribute(node, nameof(fPontoonVerticalDampingCoefficientUp));
            fPontoonVerticalDampingCoefficientDown = Xml.GetChildFloatAttribute(node, nameof(fPontoonVerticalDampingCoefficientDown));
            fKeelSphereSize = Xml.GetChildFloatAttribute(node, nameof(fKeelSphereSize));
        }
    }

    [TC(typeof(EXP))]
    public class CSpecialFlightHandlingData : SubHandlingData
    {
        public override string type => nameof(CSpecialFlightHandlingData);
        public int mode { get; set; }
        public float fLiftCoefficient { get; set; }
        public float fMinLiftVelocity { get; set; }
        public float fDragCoefficient { get; set; }
        public float fMaxPitchTorque { get; set; }
        public float fMaxSteeringRollTorque { get; set; }
        public float fMaxThrust { get; set; }
        public float fYawTorqueScale { get; set; }
        public float fRollTorqueScale { get; set; }
        public float fTransitionDuration { get; set; }
        public float fPitchTorqueScale { get; set; }
        public Vector3 vecAngularDamping { get; set; }
        public Vector3 vecAngularDampingMin { get; set; }
        public Vector3 vecLinearDamping { get; set; }
        public Vector3 vecLinearDampingMin { get; set; }
        public float fHoverVelocityScale { get; set; }
        public float fMinSpeedForThrustFalloff { get; set; }
        public float fBrakingThrustScale { get; set; }
        public string strFlags { get; set; }

        public override void Load(XmlNode node)
        {
            mode = Xml.GetChildIntInnerText(node, nameof(mode));
            fLiftCoefficient = Xml.GetChildFloatAttribute(node, nameof(fLiftCoefficient));
            fMinLiftVelocity = Xml.GetChildFloatAttribute(node, nameof(fMinLiftVelocity));
            fDragCoefficient = Xml.GetChildFloatAttribute(node, nameof(fDragCoefficient));
            fMaxPitchTorque = Xml.GetChildFloatAttribute(node, nameof(fMaxPitchTorque));
            fMaxSteeringRollTorque = Xml.GetChildFloatAttribute(node, nameof(fMaxSteeringRollTorque));
            fMaxThrust = Xml.GetChildFloatAttribute(node, nameof(fMaxThrust));
            fYawTorqueScale = Xml.GetChildFloatAttribute(node, nameof(fYawTorqueScale));
            fRollTorqueScale = Xml.GetChildFloatAttribute(node, nameof(fRollTorqueScale));
            fTransitionDuration = Xml.GetChildFloatAttribute(node, nameof(fTransitionDuration));
            fPitchTorqueScale = Xml.GetChildFloatAttribute(node, nameof(fPitchTorqueScale));
            vecAngularDamping = Xml.GetChildVector3Attributes(node, nameof(vecAngularDamping));
            vecAngularDampingMin = Xml.GetChildVector3Attributes(node, nameof(vecAngularDampingMin));
            vecLinearDamping = Xml.GetChildVector3Attributes(node, nameof(vecLinearDamping));
            vecLinearDampingMin = Xml.GetChildVector3Attributes(node, nameof(vecLinearDampingMin));
            fHoverVelocityScale = Xml.GetChildFloatAttribute(node, nameof(fHoverVelocityScale));
            fMinSpeedForThrustFalloff = Xml.GetChildFloatAttribute(node, nameof(fMinSpeedForThrustFalloff));
            fBrakingThrustScale = Xml.GetChildFloatAttribute(node, nameof(fBrakingThrustScale));
            strFlags = Xml.GetChildInnerText(node, nameof(strFlags));
        }
    }

    public class VehicleHandlingData
    {
        public string handlingName { get; set; }
        public float fMass { get; set; }
        public float fInitialDragCoeff { get; set; }
        public float fPercentSubmerged { get; set; }
        public Vector3 vecCentreOfMassOffset { get; set; }
        public Vector3 vecInertiaMultiplier { get; set; }
        public float fDriveBiasFront { get; set; }
        public int nInitialDriveGears { get; set; }
        public float fInitialDriveForce { get; set; }
        public float fDriveInertia { get; set; }
        public float fClutchChangeRateScaleUpShift { get; set; }
        public float fClutchChangeRateScaleDownShift { get; set; }
        public float fInitialDriveMaxFlatVel { get; set; }
        public float fBrakeForce { get; set; }
        public float fBrakeBiasFront { get; set; }
        public float fHandBrakeForce { get; set; }
        public float fSteeringLock { get; set; }
        public float fTractionCurveMax { get; set; }
        public float fTractionCurveMin { get; set; }
        public float fTractionCurveLateral { get; set; }
        public float fTractionSpringDeltaMax { get; set; }
        public float fLowSpeedTractionLossMult { get; set; }
        public float fCamberStiffnesss { get; set; }
        public float fTractionBiasFront { get; set; }
        public float fTractionLossMult { get; set; }
        public float fSuspensionForce { get; set; }
        public float fSuspensionCompDamp { get; set; }
        public float fSuspensionReboundDamp { get; set; }
        public float fSuspensionUpperLimit { get; set; }
        public float fSuspensionLowerLimit { get; set; }
        public float fSuspensionRaise { get; set; }
        public float fSuspensionBiasFront { get; set; }
        public float fAntiRollBarForce { get; set; }
        public float fAntiRollBarBiasFront { get; set; }
        public float fRollCentreHeightFront { get; set; }
        public float fRollCentreHeightRear { get; set; }
        public float fCollisionDamageMult { get; set; }
        public float fWeaponDamageMult { get; set; }
        public float fDeformationDamageMult { get; set; }
        public float fEngineDamageMult { get; set; }
        public float fPetrolTankVolume { get; set; }
        public float fOilVolume { get; set; }
        public float fSeatOffsetDistX { get; set; }
        public float fSeatOffsetDistY { get; set; }
        public float fSeatOffsetDistZ { get; set; }
        public int nMonetaryValue { get; set; }
        public string strModelFlags { get; set; }
        public string strHandlingFlags { get; set; }
        public string strDamageFlags { get; set; }
        public string AIHandling { get; set; }
        public SubHandlingData[] SubHandlingData { get; set; }
        public float fWeaponDamageScaledToVehHealthMult { get; set; }

        public void Load(XmlNode node)
        {
            handlingName = Xml.GetChildInnerText(node, nameof(handlingName));
            fMass = Xml.GetChildFloatAttribute(node, nameof(fMass));
            fInitialDragCoeff = Xml.GetChildFloatAttribute(node, nameof(fInitialDragCoeff));
            fPercentSubmerged = Xml.GetChildFloatAttribute(node, nameof(fPercentSubmerged));
            vecCentreOfMassOffset = Xml.GetChildVector3Attributes(node, nameof(fPercentSubmerged));
            vecInertiaMultiplier = Xml.GetChildVector3Attributes(node, nameof(vecInertiaMultiplier));
            fDriveBiasFront = Xml.GetChildFloatAttribute(node, nameof(fDriveBiasFront));
            nInitialDriveGears = Xml.GetChildIntInnerText(node, nameof(nInitialDriveGears));
            fInitialDriveForce = Xml.GetChildFloatAttribute(node, nameof(fInitialDriveForce));
            fDriveInertia = Xml.GetChildFloatAttribute(node, nameof(fDriveInertia));
            fClutchChangeRateScaleUpShift = Xml.GetChildFloatAttribute(node, nameof(fClutchChangeRateScaleUpShift));
            fClutchChangeRateScaleDownShift = Xml.GetChildFloatAttribute(node, nameof(fClutchChangeRateScaleDownShift));
            fInitialDriveMaxFlatVel = Xml.GetChildFloatAttribute(node, nameof(fInitialDriveMaxFlatVel));
            fBrakeForce = Xml.GetChildFloatAttribute(node, nameof(fBrakeForce));
            fBrakeBiasFront = Xml.GetChildFloatAttribute(node, nameof(fBrakeBiasFront));
            fHandBrakeForce = Xml.GetChildFloatAttribute(node, nameof(fHandBrakeForce));
            fSteeringLock = Xml.GetChildFloatAttribute(node, nameof(fSteeringLock));
            fTractionCurveMax = Xml.GetChildFloatAttribute(node, nameof(fTractionCurveMax));
            fTractionCurveMin = Xml.GetChildFloatAttribute(node, nameof(fTractionCurveMin));
            fTractionCurveLateral = Xml.GetChildFloatAttribute(node, nameof(fTractionCurveLateral));
            fTractionSpringDeltaMax = Xml.GetChildFloatAttribute(node, nameof(fTractionSpringDeltaMax));
            fLowSpeedTractionLossMult = Xml.GetChildFloatAttribute(node, nameof(fLowSpeedTractionLossMult));
            fCamberStiffnesss = Xml.GetChildFloatAttribute(node, nameof(fCamberStiffnesss));
            fTractionBiasFront = Xml.GetChildFloatAttribute(node, nameof(fTractionBiasFront));
            fTractionLossMult = Xml.GetChildFloatAttribute(node, nameof(fTractionLossMult));
            fSuspensionForce = Xml.GetChildFloatAttribute(node, nameof(fSuspensionForce));
            fSuspensionCompDamp = Xml.GetChildFloatAttribute(node, nameof(fSuspensionCompDamp));
            fSuspensionReboundDamp = Xml.GetChildFloatAttribute(node, nameof(fSuspensionReboundDamp));
            fSuspensionUpperLimit = Xml.GetChildFloatAttribute(node, nameof(fSuspensionUpperLimit));
            fSuspensionLowerLimit = Xml.GetChildFloatAttribute(node, nameof(fSuspensionLowerLimit));
            fSuspensionRaise = Xml.GetChildFloatAttribute(node, nameof(fSuspensionRaise));
            fSuspensionBiasFront = Xml.GetChildFloatAttribute(node, nameof(fSuspensionBiasFront));
            fAntiRollBarForce = Xml.GetChildFloatAttribute(node, nameof(fAntiRollBarForce));
            fAntiRollBarBiasFront = Xml.GetChildFloatAttribute(node, nameof(fAntiRollBarBiasFront));
            fRollCentreHeightFront = Xml.GetChildFloatAttribute(node, nameof(fRollCentreHeightFront));
            fRollCentreHeightRear = Xml.GetChildFloatAttribute(node, nameof(fRollCentreHeightRear));
            fCollisionDamageMult = Xml.GetChildFloatAttribute(node, nameof(fCollisionDamageMult));
            fWeaponDamageMult = Xml.GetChildFloatAttribute(node, nameof(fWeaponDamageMult));
            fDeformationDamageMult = Xml.GetChildFloatAttribute(node, nameof(fDeformationDamageMult));
            fEngineDamageMult = Xml.GetChildFloatAttribute(node, nameof(fEngineDamageMult));
            fPetrolTankVolume = Xml.GetChildFloatAttribute(node, nameof(fPetrolTankVolume));
            fOilVolume = Xml.GetChildFloatAttribute(node, nameof(fOilVolume));
            fSeatOffsetDistX = Xml.GetChildFloatAttribute(node, nameof(fSeatOffsetDistX));
            fSeatOffsetDistY = Xml.GetChildFloatAttribute(node, nameof(fSeatOffsetDistY));
            fSeatOffsetDistZ = Xml.GetChildFloatAttribute(node, nameof(fSeatOffsetDistZ));
            nMonetaryValue = Xml.GetChildIntInnerText(node, nameof(nMonetaryValue));
            strModelFlags = Xml.GetChildInnerText(node, nameof(strModelFlags));
            strHandlingFlags = Xml.GetChildInnerText(node, nameof(strHandlingFlags));
            strDamageFlags = Xml.GetChildInnerText(node, nameof(strDamageFlags));
            AIHandling = Xml.GetChildInnerText(node, nameof(AIHandling));
            SubHandlingData = GetSubHandlingDataArray(node, nameof(SubHandlingData));
            fWeaponDamageScaledToVehHealthMult = Xml.GetChildFloatAttribute(node, nameof(fWeaponDamageScaledToVehHealthMult));
        }

        private static SubHandlingData[] GetSubHandlingDataArray(XmlNode node, string childName)
        {
            var tempArray = new List<SubHandlingData>();
            var cnode = node.SelectSingleNode(childName);
            var items = cnode?.SelectNodes("Item");

            if (items == null) { return null; }

            foreach (XmlNode inote in items)
            {
                SubHandlingData item = null;

                var type = Xml.GetStringAttribute(inote, "type");

                switch (type)
                {
                    case nameof(CBoatHandlingData):
                        item = new CBoatHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CBikeHandlingData):
                        item = new CBikeHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CFlyingHandlingData):
                        item = new CFlyingHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CVehicleWeaponHandlingData):
                        item = new CVehicleWeaponHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CSubmarineHandlingData):
                        item = new CSubmarineHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CTrailerHandlingData):
                        item = new CTrailerHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CAdvancedData):
                        item = new CAdvancedData();
                        item.Load(inote);
                        break;
                    case nameof(CCarHandlingData):
                        item = new CCarHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CBaseSubHandlingData):
                        item = new CBaseSubHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CSeaPlaneHandlingData):
                        item = new CSeaPlaneHandlingData();
                        item.Load(inote);
                        break;
                    case nameof(CSpecialFlightHandlingData):
                        item = new CSpecialFlightHandlingData();
                        item.Load(inote);
                        break;
                }

                if (item != null)
                {
                    tempArray.Add(item);
                }
            }

            return tempArray.Count == 0 ? null : tempArray.ToArray();
        }
    }
}
