
all: plotB.svg
plotB.svg: plotB.gpi out-xydata.txt out-qSpline.txt out-qIntegral.txt out-qDerivative.txt
	gnuplot $<

out-xydata.txt out-qSpline.txt out-qIntegral.txt out-qDerivative.txt: main.exe
	mono $< 

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
matlib.dll: qspline.cs $(SRC)
	mcs $^ -target:library -out:$@


clean:
	$(RM) Out* *.txt out* *.txt *.exe *.dll *.svg
