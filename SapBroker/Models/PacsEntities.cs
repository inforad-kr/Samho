namespace SapBroker.Models;

record PacsStudy(string ComponentId, string StudyId, string AccessionNumber);

record PacsImage(int Id, string SeriesDescription, string ProtocolName);

record PacsFile(int Id);
