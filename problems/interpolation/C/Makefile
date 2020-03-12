
all: plotC.svg
plotC.svg: plotC.gpi out-xydata.txt out-cSpline.txt out-cIntegral.txt out-cDerivative.txt
	gnuplot $<

out-xydata.txt out-cSpline.txt out-cIntegral.txt out-cDerivative.txt: main.exe
	mono $< 

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
matlib.dll: cspline.cs $(SRC)
	mcs $^ -target:library -out:$@


clean:
	$(RM) out* *.txt *.exe *.dll *.svg
