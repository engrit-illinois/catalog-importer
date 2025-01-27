namespace Application.Common.Constants;

public static class Degrees
{
    public static readonly Department AerospaceEngineering = new()
    {
        Name = "Aerospace Engineering",
        DegreeEntries = {
            new() { Name = "Aerospace Engineering, BS", MajorCode = "1647", CatalogYear = 2024, CatalogUrl = "engineering/aerospace-engineering-bs" },
        }
    };

    public static readonly Department AgriculturalAndBiologicalEngineering = new()
    {
        Name = "Agricultural & Biological Engineering",
        DegreeEntries =
        {
            new() { Name = "Agricultural & Biological Engineering, BS", MajorCode = "5163", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs" },
            new() { Name = "Agricultural & Biological Engineering: Bioprocess Engineering and Industrial Biotechnology", MajorCode = "5163", ConcentrationCode = "6223", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/bioprocess-engineering-industrial-biotechnology" },
            new() { Name = "Agricultural & Biological Engineering: Off-Highway Vehicle and Equipment Engineering", MajorCode = "5163", ConcentrationCode = "6221", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/off-highway-vehicle-equipment-engineering" },
            new() { Name = "Agricultural & Biological Engineering: Renewable Energy Systems Engineering", MajorCode = "5163", ConcentrationCode = "6222", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/renewable-energy-systems-engineering" },
            new() { Name = "Agricultural & Biological Engineering: Soil and Water Resources Engineering", MajorCode = "5163", ConcentrationCode = "6218", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/soil-water-resources-engineering" },
            new() { Name = "Agricultural & Biological Engineering: Sustainable Ecological and Environmental Systems Engineering", MajorCode = "5163", ConcentrationCode = "6219", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/sustainable-ecological-environmental-systems-engineering" },
            new() { Name = "Agricultural & Biological Engineering: Synthetic Biological Engineering", MajorCode = "5163", ConcentrationCode = "6220", CatalogYear = 2024, CatalogUrl = "eng_aces/agricultural-biological-engineering-bs/synthetic-biological-engineering" }
        }
    };

    public static readonly Department Bioengineering = new()
    {
        Name = "Bioengineering",
        DegreeEntries = {
            new() { Name = "Bioengineering, BS", MajorCode = "0408", CatalogYear = 2024, CatalogUrl = "engineering/bioengineering-bs" }
        }
    };

    public static readonly Department CivilAndEnvironmentalEngineering = new()
    {
        Name = "Civil and Environmental Engineering",
        DegreeEntries = {
            new() { Name = "Civil Engineering, BS", MajorCode = "0106", CatalogYear = 2024, CatalogUrl = "engineering/civil-engineering-bs" },
            new() { Name = "Environmental Engineering, BS", MajorCode = "1233", CatalogYear = 2024, CatalogUrl = "engineering/environmental-engineering-bs" }
        }
    };

    public static readonly Department ChemicalAndBiomolecularEngineering = new()
    {
        Name = "Chemical & Biomolecular Engineering",
        DegreeEntries = {
            //TODO
        }
    };

    public static readonly Department ElectricalAndComputerAndEngineering = new()
    {
        Name = "Electrical & Computer Engineering",
        DegreeEntries = {
            new() { Name = "Computer Engineering, BS", MajorCode = "0109", CatalogYear = 2024, CatalogUrl = "engineering/computer-engineering-bs" }
        }
    };

    public static readonly Department ComputerScience = new()
    {
        Name = "Siebel School of Computing and Data Science",
        DegreeEntries = {
            new() { Name = "Computer Science, BS", MajorCode = "0112", CatalogYear = 2024, CatalogUrl = "engineering/computer-science-bs" },
            new() { Name = "Computer Science + Animal Sciences, BS", MajorCode = "5864", CatalogYear = 2024, CatalogUrl = "aces/computer-science-animal-sciences-bs" },
            new() { Name = "Computer Science + Anthropology, BSLAS", MajorCode = "5348", CatalogYear = 2024, CatalogUrl = "eng_las/computer-science-anthropology-bslas" },
            new() { Name = "Computer Science + Astronomy, BSLAS", MajorCode = "5349", CatalogYear = 2024, CatalogUrl = "eng_las/computer-science-astronomy-bslas" },
            new() { Name = "Computer Science + Bioengineering, BS", MajorCode = "6151", CatalogYear = 2024, CatalogUrl = "engineering/computer-science-bioengineering-bs" },
            new() { Name = "Computer Science + Chemistry, BSLAS", MajorCode = "5350", CatalogYear = 2024, CatalogUrl = "eng_las/computer-science-chemistry-bslas" },
            new() { Name = "Computer Science + Crop Sciences, BS", MajorCode = "5623", CatalogYear = 2024, CatalogUrl = "aces/computer-science-crop-sciences-bs" },
            new() { Name = "Computer Science + Economics, BSLAS", MajorCode = "5667", CatalogYear = 2024, CatalogUrl = "eng_las/computer-science-economics-bslas" },
        }
    };

    public static readonly IReadOnlyList<Department> AllDepartments = new List<Department>
    {
        AerospaceEngineering,
        AgriculturalAndBiologicalEngineering,
        Bioengineering,
        CivilAndEnvironmentalEngineering,
        ComputerScience,
        ElectricalAndComputerAndEngineering,
    }
    .OrderBy(d => d.Name).ToList();





    //public static DegreeProgram EE = new() { Name = "Electrical Engineering, BS", MajorCode = "0115", CatalogYear = 2024, CatalogUrl = "engineering/electrical-engineering-bs" };
    //public static DegreeProgram EM = new() { Name = "Engineering Mechanics, BS", MajorCode = "0118", CatalogYear = 2024, CatalogUrl = "engineering/engineering-mechanics-bs" };
    //public static DegreeProgram EngineeringUndeclared = new() { Name = "Engineering Undeclared", MajorCode = "5333", CatalogYear = 2024, CatalogUrl = "engineering/engineering-undeclared" };
    //public static DegreeProgram EnvironmentalEngineeringBS = new() { Name = "Environmental Engineering, BS", MajorCode = "1233", CatalogYear = 2024, CatalogUrl = "engineering/environmental-engineering-bs" };
    //public static DegreeProgram IndustrialEngineeringBS = new() { Name = "Industrial Engineering, BS", MajorCode = "0127", CatalogYear = 2024, CatalogUrl = "engineering/industrial-engineering-bs" };
    //public static DegreeProgram InnovationLeadershipEngineeringEntrepreneurshipBS = new() { Name = "Innovation, Leadership & Engineering Entrepreneurship, BS (ILEE)", MajorCode = "5546", CatalogYear = 2024, CatalogUrl = "engineering/innovation-leadership-engineering-entrepreneurship-bs" };
    //public static DegreeProgram MaterialsScienceEngineeringBS = new() { Name = "Materials Science & Engineering, BS", MajorCode = "0130", CatalogYear = 2024, CatalogUrl = "engineering/materials-science-engineering-bs" };
    //public static DegreeProgram MathematicsComputerScienceBSLAS = new() { Name = "Mathematics & Computer Science, BSLAS", MajorCode = "1438", CatalogYear = 2024, CatalogUrl = "eng_las/mathematics-computer-science-bslas" };
    //public static DegreeProgram MechanicalEngineeringBS = new() { Name = "Mechanical Engineering, BS", MajorCode = "0133", CatalogYear = 2024, CatalogUrl = "engineering/mechanical-engineering-bs" };
    //public static DegreeProgram NeuralEngineeringBS = new() { Name = "Neural Engineering, BS", MajorCode = "6131", CatalogYear = 2024, CatalogUrl = "engineering/neural-engineering-bs" };
    //public static DegreeProgram NuclearPlasmaRadiologicalEngineeringBS = new() { Name = "Nuclear, Plasma, & Radiological Engineering", MajorCode = "5183", CatalogYear = 2024, CatalogUrl = "engineering/nuclear-plasma-radiological-engineering-bs/" };
    //public static DegreeProgram NuclearPlasmaRadiologicalEngineeringPlasmaFusionScienceEngineering = new() { Name = "Nuclear, Plasma, & Radiological Engineering: Plasma & Fusion Science & Engineering", MajorCode = "5183", ConcentrationCode = "5611", CatalogYear = 2024, CatalogUrl = "engineering/nuclear-plasma-radiological-engineering-bs/plasma-fusion-science-engineering" };
    //public static DegreeProgram NuclearPlasmaRadiologicalEngineeringPowerSafetyEnvironment = new() { Name = "Nuclear, Plasma, & Radiological Engineering: Power, Safety & Environment", MajorCode = "5183", ConcentrationCode = "3913", CatalogYear = 2024, CatalogUrl = "engineering/nuclear-plasma-radiological-engineering-bs/power-safety-environment" };
    //public static DegreeProgram NuclearPlasmaRadiologicalEngineeringRadiologicalMedicalInstrumentationApplications = new() { Name = "Nuclear, Plasma, & Radiological Engineering: Radiological, Medical & Instrumentation Applications", MajorCode = "5183", ConcentrationCode = "3915", CatalogYear = 2024, CatalogUrl = "engineering/nuclear-plasma-radiological-engineering-bs/radiological-engineering-radiological-medical-instrumentation-applications" };
    //public static DegreeProgram PhysicsBS = new() { Name = "Physics, BS", MajorCode = "0121", CatalogYear = 2024, CatalogUrl = "engineering/physics-bs" };
    //public static DegreeProgram StatisticsComputerScienceBSLAS = new() { Name = "Statistics & Computer Science, BSLAS", MajorCode = "0464", CatalogYear = 2024, CatalogUrl = "eng_las/statistics-computer-science-bslas" };


}
