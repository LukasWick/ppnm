all: OutB.txt PlotB.svg
	cat OutB.txt


PlotB.svg: plotB.gpi out.plotB.txt
	gnuplot $<

OutB.txt out.plotB.txt: mainB.exe
	mono $< >$@

mainB.exe: mainB.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../../2-linear-equations/gr.gs.cs
SRC +=../../1-interpolation/C/cspline.cs
SRC +=../../5-ode/ode.integrator.cs
SRC +=../rootfinder.cs
matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) Out* out* *.exe *.dll *.svg *.txt
