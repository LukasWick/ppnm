all: PlotC.svg

PlotC.svg: plotC.gpi out.solvedFun.sin.txt 
	gnuplot plotC.gpi

out.solvedFun.sin.txt: mainC.exe
	mono $< 1> log.txt

mainC.exe: mainC.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../../6-quadratures/integrator.cs
SRC +=../../8-multimin/qnewton.cs

SRC +=nndiffeqsolver.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg Out* *.txt
