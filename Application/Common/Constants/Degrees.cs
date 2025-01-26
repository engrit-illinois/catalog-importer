namespace Application.Common.Constants;

public static class Degrees
{
    public static readonly Department CivilAndEnvironmentalEngineering = new()
    {
        Name = "Department of Civil and Environmental Engineering",
        CatalogKey = "engineering",
        DegreeEntries = {
            new() { Name = "Civil Engineering, BS", MajorCode = "0106", CatalogYear = 2024, CatalogUrl = "engineering/civil-engineering-bs" },
            new() { Name = "Environmental Engineering, BS", MajorCode = "1233", CatalogYear = 2024, CatalogUrl = "engineering/environmental-engineering-bs" }
        }
    };

    public static readonly Department AerospaceEngineering = new()
    {
        Name = "Aerospace Engineering",
        CatalogKey = "engineering",
        DegreeEntries = {
            new() { Name = "Aerospace Engineering, BS", MajorCode = "1647", CatalogYear = 2024, CatalogUrl = "engineering/aerospace-engineering-bs" },
        }
    };

    public static readonly Department AgriculturalAndBiologicalEngineering = new()
    {
        Name = "Agricultural & Biological Engineering",
        CatalogKey = "eng_aces",
        DegreeEntries =
        {
            new() { Name = "Agricultural & Biological Engineering: Bioprocess Engineering and Industrial Biotechnology", MajorCode = "5163", ConcentrationCode = "6223", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/bioprocess-engineering-industrial-biotechnology" },
            new() { Name = "Agricultural & Biological Engineering: Off-Highway Vehicle and Equipment Engineering", MajorCode = "5163", ConcentrationCode = "6221", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/off-highway-vehicle-equipment-engineering" },
            new() { Name = "Agricultural & Biological Engineering: Renewable Energy Systems Engineering", MajorCode = "5163", ConcentrationCode = "6222", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/renewable-energy-systems-engineering" },
            new() { Name = "Agricultural & Biological Engineering: Soil and Water Resources Engineering", MajorCode = "5163", ConcentrationCode = "6218", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/soil-water-resources-engineering" },
            new() { Name = "Agricultural & Biological Engineering: Sustainable Ecological and Environmental Systems Engineering", MajorCode = "5163", ConcentrationCode = "6219", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/sustainable-ecological-environmental-systems-engineering" },
            new() { Name = "Agricultural & Biological Engineering: Synthetic Biological Engineering", MajorCode = "5163", ConcentrationCode = "6220", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/synthetic-biological-engineering" }
        }
    };

    public static readonly IReadOnlyList<Department> AllDepartments = new List<Department>
    {
        AerospaceEngineering,
        AgriculturalAndBiologicalEngineering,
        CivilAndEnvironmentalEngineering,
    }
    .OrderBy(d => d.Name).ToList();

    //public static DegreeProgram AERO = new() { Name = "Aerospace Engineering, BS", MajorCode = "4048", CatalogUri = "engineering/aerospace-engineering-bs" };
    //public static DegreeProgram ABE = new() { Name = "Agricultural & Biological Engineering, BS", MajorCode = "5163", CatalogUri = "eng_aces/agricultural-biological-engineering-bs" };
    //public static DegreeProgram ABE_Bioprocess = new() { Name = "Agricultural & Biological Engineering: Bioprocess Engineering and Industrial Biotechnology", MajorCode = "5163", ConcentrationCode = "6223", CatalogUri = "eng_aces/agricultural-biological-engineering-bs/bioprocess-engineering-industrial-biotechnology" };
    //public static DegreeProgram ABE_Off_Highway = new() { Name = "Agricultural & Biological Engineering: Off-Highway Vehicle and Equipment Engineering", MajorCode = "5163", ConcentrationCode = "6221", CatalogUri = "eng_aces/agricultural-biological-engineering-bs/off-highway-vehicle-equipment-engineering" };
    //public static DegreeProgram ABE_Renewable_Energy = new() { Name = "Agricultural & Biological Engineering: Renewable Energy Systems Engineering", MajorCode = "5163", ConcentrationCode = "6222", CatalogUri = "eng_aces/agricultural-biological-engineering-bs/renewable-energy-systems-engineering" };
    //public static DegreeProgram ABE_Soil_Water = new() { Name = "Agricultural & Biological Engineering: Soil and Water Resources Engineering", MajorCode = "5163", ConcentrationCode = "6218", CatalogUri = "eng_aces/agricultural-biological-engineering-bs/soil-water-resources-engineering" };
    //public static DegreeProgram ABE_Sustainable_Eco = new() { Name = "Agricultural & Biological Engineering: Sustainable Ecological and Environmental Systems Engineering", MajorCode = "5163", ConcentrationCode = "6219", CatalogUri = "eng_aces/agricultural-biological-engineering-bs/sustainable-ecological-environmental-systems-engineering" };
    //public static DegreeProgram ABE_Syntehtic_Bio = new() { Name = "Agricultural & Biological Engineering: Synthetic Biological Engineering", MajorCode = "5163", ConcentrationCode = "6220", CatalogUri = "eng_aces/agricultural-biological-engineering-bs/synthetic-biological-engineering" };
    //public static DegreeProgram BIOE = new() { Name = "Bioengineering, BS", MajorCode = "0408", CatalogUri = "engineering/bioengineering-bs" };
    //public static DegreeProgram CE = new() { Name = "Civil Engineering, BS", MajorCode = "0106", CatalogUri = "engineering/civil-engineering-bs" };
    //public static DegreeProgram COMPE = new() { Name = "Computer Engineering, BS", MajorCode = "0109", CatalogUri = "engineering/computer-engineering-bs" };
    //public static DegreeProgram CS = new() { Name = "Computer Science, BS", MajorCode = "0112", CatalogUri = "engineering/computer-science-bs" };
    //public static DegreeProgram CS_Animal_Science = new() { Name = "Computer Science + Animal Sciences, BS", MajorCode = "5864", CatalogUri = "aces/computer-science-animal-sciences-bs" };
    //public static DegreeProgram CS_Anthropology = new() { Name = "Computer Science + Anthropology, BSLAS", MajorCode = "5348", CatalogUri = "eng_las/computer-science-anthropology-bslas" };
    //public static DegreeProgram CS_Astronomy = new() { Name = "Computer Science + Astronomy, BSLAS", MajorCode = "5349", CatalogUri = "eng_las/computer-science-astronomy-bslas" };
    //public static DegreeProgram CS_Bioengineering = new() { Name = "Computer Science + Bioengineering, BS", MajorCode = "6151", CatalogUri = "engineering/computer-science-bioengineering-bs" };
    //public static DegreeProgram CS_Chemistry = new() { Name = "Computer Science + Chemistry, BSLAS", MajorCode = "5350", CatalogUri = "eng_las/computer-science-chemistry-bslas" };
    //public static DegreeProgram CS_Crop_Sciences = new() { Name = "Computer Science + Crop Sciences, BS", MajorCode = "5623", CatalogUri = "aces/computer-science-crop-sciences-bs" };
    //public static DegreeProgram CS_Economics = new() { Name = "Computer Science + Economics, BSLAS", MajorCode = "5667", CatalogUri = "eng_las/computer-science-economics-bslas" };



    //public static DegreeProgram EE = new() { Name = "Electrical Engineering, BS", MajorCode = "0115", CatalogUri = "engineering/electrical-engineering-bs" };
    //public static DegreeProgram EM = new() { Name = "Engineering Mechanics, BS", MajorCode = "0118", CatalogUri = "engineering/engineering-mechanics-bs" };
    //public static DegreeProgram EngineeringUndeclared = new() { Name = "Engineering Undeclared", MajorCode = "5333", CatalogUri = "engineering/engineering-undeclared" };
    //public static DegreeProgram EnvironmentalEngineeringBS = new() { Name = "Environmental Engineering, BS", MajorCode = "1233", CatalogUri = "engineering/environmental-engineering-bs" };
    //public static DegreeProgram IndustrialEngineeringBS = new() { Name = "Industrial Engineering, BS", MajorCode = "0127", CatalogUri = "engineering/industrial-engineering-bs" };
    //public static DegreeProgram InnovationLeadershipEngineeringEntrepreneurshipBS = new() { Name = "Innovation, Leadership & Engineering Entrepreneurship, BS (ILEE)", MajorCode = "5546", CatalogUri = "engineering/innovation-leadership-engineering-entrepreneurship-bs" };
    //public static DegreeProgram MaterialsScienceEngineeringBS = new() { Name = "Materials Science & Engineering, BS", MajorCode = "0130", CatalogUri = "engineering/materials-science-engineering-bs" };
    //public static DegreeProgram MathematicsComputerScienceBSLAS = new() { Name = "Mathematics & Computer Science, BSLAS", MajorCode = "1438", CatalogUri = "eng_las/mathematics-computer-science-bslas" };
    //public static DegreeProgram MechanicalEngineeringBS = new() { Name = "Mechanical Engineering, BS", MajorCode = "0133", CatalogUri = "engineering/mechanical-engineering-bs" };
    //public static DegreeProgram NeuralEngineeringBS = new() { Name = "Neural Engineering, BS", MajorCode = "6131", CatalogUri = "engineering/neural-engineering-bs" };
    //public static DegreeProgram NuclearPlasmaRadiologicalEngineeringBS = new() { Name = "Nuclear, Plasma, & Radiological Engineering", MajorCode = "5183", CatalogUri = "engineering/nuclear-plasma-radiological-engineering-bs/" };
    //public static DegreeProgram NuclearPlasmaRadiologicalEngineeringPlasmaFusionScienceEngineering = new() { Name = "Nuclear, Plasma, & Radiological Engineering: Plasma & Fusion Science & Engineering", MajorCode = "5183", ConcentrationCode = "5611", CatalogUri = "engineering/nuclear-plasma-radiological-engineering-bs/plasma-fusion-science-engineering" };
    //public static DegreeProgram NuclearPlasmaRadiologicalEngineeringPowerSafetyEnvironment = new() { Name = "Nuclear, Plasma, & Radiological Engineering: Power, Safety & Environment", MajorCode = "5183", ConcentrationCode = "3913", CatalogUri = "engineering/nuclear-plasma-radiological-engineering-bs/power-safety-environment" };
    //public static DegreeProgram NuclearPlasmaRadiologicalEngineeringRadiologicalMedicalInstrumentationApplications = new() { Name = "Nuclear, Plasma, & Radiological Engineering: Radiological, Medical & Instrumentation Applications", MajorCode = "5183", ConcentrationCode = "3915", CatalogUri = "engineering/nuclear-plasma-radiological-engineering-bs/radiological-engineering-radiological-medical-instrumentation-applications" };
    //public static DegreeProgram PhysicsBS = new() { Name = "Physics, BS", MajorCode = "0121", CatalogUri = "engineering/physics-bs" };
    //public static DegreeProgram StatisticsComputerScienceBSLAS = new() { Name = "Statistics & Computer Science, BSLAS", MajorCode = "0464", CatalogUri = "eng_las/statistics-computer-science-bslas" };


}
