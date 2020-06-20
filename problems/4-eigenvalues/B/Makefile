all: PlotB1.svg Out.B.txt PlotB34.svg
	cat Out.B.txt

PlotB1.svg: plotB1.gpi out.plotB1.data
	gnuplot $<

PlotB34.svg: plotB34.gpi out.plotB34vbv1.data out.plotB34vbvn.data out.plotB34cyclic.data
	gnuplot $<

out.plotB1.data: mainB1.exe
	mono $< > $@

Out.B.txt out.plotB34vbv1.data out.plotB34vbvn.data out.plotB34cyclic.data: mainB2.exe
	mono $< > $@

mainB1.exe: mainB1.cs matlib.dll
	mcs $< -reference:$(lastword $^)

mainB2.exe: mainB2.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs


matlib.dll: ../jac.diag.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) Out* *.txt out* *.exe *.dll *.svg *.log
