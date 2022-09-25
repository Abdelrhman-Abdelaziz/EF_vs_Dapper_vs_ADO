using BenchmarkDotNet.Running;
using EF_vs_Dapper_vs_ADO.BenchMark;


var summary = BenchmarkRunner.Run<DataAcessBenchMark>();