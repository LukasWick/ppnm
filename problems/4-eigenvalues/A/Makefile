all: OutA1.txt PlotA.psi.svg PlotA.E.svg
	cat OutA1.txt

PlotA.psi.svg: plotA.psi.gpi out.plotA.psi.txt
	gnuplot $<

PlotA.E.svg: plotA.E.gpi out.plotA.E.txt
	gnuplot $<

OutA1.txt out.plotA.E.txt out.plotA.psi.txt: mainA.exe
	mono $< > $@

mainA.exe: mainA.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs


matlib.dll: ../jac.diag.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) Out* *.txt out* *.exe *.dll *.svg
