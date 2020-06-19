all: PlotC.svg Out.C.txt 
	cat Out.C.txt

PlotC.svg: PlotC.gpi out.plotC.time.data out.plotC.rotations.data
	gnuplot $<

Out.C.txt out.plotC.time.data out.plotC.rotations.data: mainC.exe
	mono $< > $@

mainC.exe: mainC.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs

matlib.dll: ../jac.diag.cs  $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg *.log Out*
