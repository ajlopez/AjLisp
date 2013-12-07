using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLisp.Language;

namespace AjLisp.Tests
{
    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void MachineEvaluateNull()
        {
            Assert.IsNull(Machine.Evaluate(null, null));
        }

        [TestMethod]
        public void MachineEvaluateSimpleObjects()
        {
            Assert.AreEqual(1, Machine.Evaluate(1, null));
            Assert.AreEqual(3.1416, Machine.Evaluate(3.1416, null));
            Assert.AreEqual("foo", Machine.Evaluate("foo", null));
            Assert.AreEqual(true, Machine.Evaluate(true, null));
            Assert.AreEqual(false, Machine.Evaluate(false, null));
        }

        [TestMethod]
        public void EvaluateNil()
        {
            this.EvaluateAndCompare("nil", "nil");
        }

        [TestMethod]
        public void EvaluateTrue()
        {
            this.EvaluateAndCompare("t", "t");
        }

        [TestMethod]
        public void EvaluateNilPredicate()
        {
            this.EvaluateAndCompare("(nilp nil)", "t");
            this.EvaluateAndCompare("(nilp t)", "nil");
        }

        [TestMethod]
        public void EvaluateAtomNil()
        {
            this.EvaluateAndCompare("(atom nil)", "t");
        }

        [TestMethod]
        public void EvaluateAtomTrue()
        {
            this.EvaluateAndCompare("(atom t)", "t");
        }

        [TestMethod]
        public void EvaluateAtomQuotedAtom()
        {
            this.EvaluateAndCompare("(atom 'a)", "t");
        }

        [TestMethod]
        public void EvaluateAtomInteger()
        {
            this.EvaluateAndCompare("(atom 12)", "t");
        }

        [TestMethod]
        public void EvaluateAtomQuotedList()
        {
            this.EvaluateAndCompare("(atom '(a b))", "nil");
        }

        [TestMethod]
        public void EvaluateAtomEmptyList()
        {
            this.EvaluateAndCompare("(atom ())", "t");
        }

        [TestMethod]
        public void EvaluateNullNil()
        {
            this.EvaluateAndCompare("(null nil)", "t");
        }

        [TestMethod]
        public void EvaluateNullTrue()
        {
            this.EvaluateAndCompare("(null t)", "nil");
        }

        [TestMethod]
        public void EvaluateNullQuotedAtom()
        {
            this.EvaluateAndCompare("(null 'a)", "nil");
        }

        [TestMethod]
        public void EvaluateNullQuotedList()
        {
            this.EvaluateAndCompare("(null '(a b))", "nil");
        }

        [TestMethod]
        public void EvaluateQuotedAtom()
        {
            this.EvaluateAndCompare("'a", "a");
        }

        [TestMethod]
        public void EvaluateExplicitQuotedAtom()
        {
            this.EvaluateAndCompare("(quote a)", "a");
        }

        [TestMethod]
        public void EvaluateExplicitQuotedList()
        {
            this.EvaluateAndCompare("(quote (a b))", "(a b)");
        }

        [TestMethod]
        public void EvaluateExplicitQuotedNil()
        {
            this.EvaluateAndCompare("(quote nil)", "nil");
        }

        [TestMethod]
        public void EvaluateExplicitQuotedQuoteAtom()
        {
            this.EvaluateAndCompare("(quote quote)", "quote");
        }

        [TestMethod]
        public void EvaluateEvalNil()
        {
            this.EvaluateAndCompare("(eval nil)", "nil");
        }

        [TestMethod]
        public void EvaluateEvalQuotedNil()
        {
            this.EvaluateAndCompare("(eval 'nil)", "nil");
        }

        [TestMethod]
        public void EvaluateEvalTrue()
        {
            this.EvaluateAndCompare("(eval t)", "t");
        }

        [TestMethod]
        public void EvaluateEvalInteger()
        {
            this.EvaluateAndCompare("(eval 1)", "1");
        }

        [TestMethod]
        public void EvaluateEvalQuotedPlusTwoIntegers()
        {
            this.EvaluateAndCompare("(eval '(plus 1 2))", "3");
        }

        [TestMethod]
        public void EvaluateEvalQuotedAtomQuotedAtom()
        {
            this.EvaluateAndCompare("(eval '(atom 'a))", "t");
        }

        [TestMethod]
        public void EvaluateEvalQuotedFirstQuotedList()
        {
            this.EvaluateAndCompare("(eval '(first '(a b)))", "a");
        }

        [TestMethod]
        public void EvaluateEvalQuotedRestQuotedList()
        {
            this.EvaluateAndCompare("(eval '(rest '(a b)))", "(b)");
        }

        [TestMethod]
        public void EvaluateListQuotedAtom()
        {
            this.EvaluateAndCompare("(list 'a)", "(a)");
        }

        [TestMethod]
        public void EvaluateListTwoQuotedAtoms()
        {
            this.EvaluateAndCompare("(list 'a 'b)", "(a b)");
        }

        [TestMethod]
        public void EvaluateFirstListTwoQuotedAtoms()
        {
            this.EvaluateAndCompare("(first (list 'a 'b))", "a");
        }

        [TestMethod]
        public void EvaluateFirstQuotedList()
        {
            this.EvaluateAndCompare("(first '(a b))", "a");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseOnFirstQuotedAtom()
        {
            this.EvaluateAndCompare("(first 'a)", "a");
        }

        [TestMethod]
        public void EvaluateFirstNilAsNil()
        {
            this.EvaluateAndCompare("(first nil)", "nil");
        }

        [TestMethod]
        public void EvaluateCarList()
        {
            this.EvaluateAndCompare("(car (list 'a 'b))", "a");
        }

        [TestMethod]
        public void EvaluateCarQuotedList()
        {
            this.EvaluateAndCompare("(car '(a b))", "a");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseOnEvaluateCarQuotedAtom()
        {
            this.EvaluateAndCompare("(car 'a)", "a");
        }

        [TestMethod]
        public void EvaluateCarNilAsNil()
        {
            this.EvaluateAndCompare("(car nil)", "nil");
        }

        [TestMethod]
        public void EvaluateRestListTwoQuotedAtoms()
        {
            this.EvaluateAndCompare("(rest (list 'a 'b))", "(b)");
        }

        [TestMethod]
        public void EvaluateRestQuotedList()
        {
            this.EvaluateAndCompare("(rest '(a b))", "(b)");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseOnRestQuotedAtom()
        {
            this.EvaluateAndCompare("(rest 'a)", "a");
        }

        [TestMethod]
        public void EvaluateCdrListTwoQuotedAtoms()
        {
            this.EvaluateAndCompare("(cdr (list 'a 'b))", "(b)");
        }

        [TestMethod]
        public void EvaluateCdrQuotedList()
        {
            this.EvaluateAndCompare("(cdr '(a b))", "(b)");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseOnCdrQuotedAtom()
        {
            this.EvaluateAndCompare("(cdr 'a)", "a");
        }

        [TestMethod]
        public void EvaluateRestNilAsNil()
        {
            this.EvaluateAndCompare("(rest nil)", "nil");
        }

        [TestMethod]
        public void EvaluateConsQuotedAtomNil()
        {
            this.EvaluateAndCompare("(cons 'a nil)", "(a)");
        }

        [TestMethod]
        public void EvaluateConsQuotedAtomQuotedList()
        {
            this.EvaluateAndCompare("(cons 'a '(b))", "(a b)");
        }

        [TestMethod]
        public void EvaluateConsQuotedLists()
        {
            this.EvaluateAndCompare("(cons '(a) '(b))", "((a) b)");
        }

        [TestMethod]
        public void EvaluateConsPredicateNil()
        {
            this.EvaluateAndCompare("(consp nil)", "nil");
        }

        [TestMethod]
        public void EvaluateConsPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(consp 'a)", "nil");
        }

        [TestMethod]
        public void EvaluateConsPredicateQuotedList()
        {
            this.EvaluateAndCompare("(consp '(a))", "t");
        }

        [TestMethod]
        public void EvaluateConsPredicateCons()
        {
            this.EvaluateAndCompare("(consp (cons 'a '(b)))", "t");
        }

        [TestMethod]
        public void EvaluateIdPredicateNil()
        {
            this.EvaluateAndCompare("(idp nil)", "t");
        }

        [TestMethod]
        public void EvaluateIdPredicateTrue()
        {
            this.EvaluateAndCompare("(idp t)", "t");
        }

        [TestMethod]
        public void EvaluateIdPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(idp 'a)", "t");
        }

        [TestMethod]
        public void EvaluateIdPredicateQuotedList()
        {
            this.EvaluateAndCompare("(idp '(a b))", "nil");
        }

        [TestMethod]
        public void EvaluateIdPredicateQuotedInteger()
        {
            this.EvaluateAndCompare("(idp 1)", "nil");
        }

        [TestMethod]
        public void EvaluateIdPredicatePlus()
        {
            this.EvaluateAndCompare("(idp (plus 1 2))", "nil");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateNil()
        {
            this.EvaluateAndCompare("(functionp nil)", "nil");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(functionp 'a)", "nil");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateAtom()
        {
            this.EvaluateAndCompare("(functionp atom)", "t");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateLambda()
        {
            this.EvaluateAndCompare("(functionp (lambda (x) x))", "t");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateIf()
        {
            this.EvaluateAndCompare("(functionp if)", "t");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateLambdaAtom()
        {
            this.EvaluateAndCompare("(functionp lambda)", "t");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateMacroLambda()
        {
            this.EvaluateAndCompare("(functionp mlambda)", "t");
        }

        [TestMethod]
        public void EvaluateNumberPredicateNil()
        {
            this.EvaluateAndCompare("(numberp nil)", "nil");
        }

        [TestMethod]
        public void EvaluateNumberPredicateTrue()
        {
            this.EvaluateAndCompare("(numberp t)", "nil");
        }

        [TestMethod]
        public void EvaluateNumberPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(numberp 'a)", "nil");
        }

        [TestMethod]
        public void EvaluateNumberPredicateQuotedList()
        {
            this.EvaluateAndCompare("(numberp '(a b))", "nil");
        }

        [TestMethod]
        public void EvaluateNumberPredicateInteger()
        {
            this.EvaluateAndCompare("(numberp 1)", "t");
        }

        [TestMethod]
        public void EvaluateNumberPredicatePlus()
        {
            this.EvaluateAndCompare("(numberp (plus 1 2))", "t");
        }

        [TestMethod]
        public void EvaluateEqPredicateNilNil()
        {
            this.EvaluateAndCompare("(eq nil nil)", "t");
        }

        [TestMethod]
        public void EvaluateEqPredicateTrueTrue()
        {
            this.EvaluateAndCompare("(eq t t)", "t");
        }

        [TestMethod]
        public void EvaluateEqPredicateQuotedAtoms()
        {
            this.EvaluateAndCompare("(eq 'a 'a)", "t");
        }

        [TestMethod]
        public void EvaluateEqPredicateQuotedDifferentAtoms()
        {
            this.EvaluateAndCompare("(eq 'a 'b)", "nil");
        }

        [TestMethod]
        public void EvaluateEqPredicateIntegers()
        {
            this.EvaluateAndCompare("(eq 12 20)", "nil");
        }

        [TestMethod]
        public void EvaluateEqPredicateOneOne()
        {
            this.EvaluateAndCompare("(eq 1 1)", "t");
        }

        [TestMethod]
        public void EvaluateEqPredicateIntegerPlus()
        {
            this.EvaluateAndCompare("(eq 3 (plus 1 2))", "t");
        }

        [TestMethod]
        public void EvaluateGreaterPredicateTwoOne()
        {
            this.EvaluateAndCompare("(greater 2 1)", "t");
        }

        [TestMethod]
        public void EvaluateGreaterPredicateOneTwo()
        {
            this.EvaluateAndCompare("(greater 1 2)", "nil");
        }

        [TestMethod]
        public void EvaluateGreaterPredicateAtomsAB()
        {
            this.EvaluateAndCompare("(greater 'a 'b)", "nil");
        }

        [TestMethod]
        public void EvaluateGreaterPredicateAtomsBA()
        {
            this.EvaluateAndCompare("(greater 'b 'a)", "t");
        }

        [TestMethod]
        public void EvaluateLessPredicateTwoOne()
        {
            this.EvaluateAndCompare("(less 2 1)", "nil");
        }

        [TestMethod]
        public void EvaluateLessPredicateOneTwo()
        {
            this.EvaluateAndCompare("(less 1 2)", "t");
        }

        [TestMethod]
        public void EvaluateLessPredicateAtomsAB()
        {
            this.EvaluateAndCompare("(less 'a 'b)", "t");
        }

        [TestMethod]
        public void EvaluateLessPredicateAtomsBA()
        {
            this.EvaluateAndCompare("(less 'b 'a)", "nil");
        }

        [TestMethod]
        public void EvaluateProgNTrue()
        {
            this.EvaluateAndCompare("(progn t)", "t");
        }

        [TestMethod]
        public void EvaluateProgNNil()
        {
            this.EvaluateAndCompare("(progn nil)", "nil");
        }

        [TestMethod]
        public void EvaluateProgNAtoms()
        {
            this.EvaluateAndCompare("(progn 'a 'b)", "b");
        }

        [TestMethod]
        public void EvaluateProgNTwoExpressions()
        {
            this.EvaluateAndCompare("(progn (plus 1 2) (plus 3 4))", "7");
        }

        [TestMethod]
        public void EvaluateProgNThreeExpressions()
        {
            this.EvaluateAndCompare("(progn (plus 1 2) (plus 3 4) (plus 5 6))", "11");
        }

        [TestMethod]
        public void EvaluateLambdaFirstList()
        {
            this.EvaluateAndCompare("((lambda (x) (first x)) '(a))", "a");
        }

        [TestMethod]
        public void EvaluateLambdaWithSet()
        {
            Machine intr = new Machine();
            intr.Evaluate("(set 'firstone (lambda (x) (first x)))");
            this.EvaluateAndCompare(intr, "(firstone '(a b))", "a");
        }

        [TestMethod]
        public void EvaluateLambdaWithFreeAtom()
        {
            Machine machine = new Machine();
            machine.Evaluate("(set 'free (lambda (x) y))");
            machine.Evaluate("(set 'y 'a)");
            this.EvaluateAndCompare(machine, "(free '(d c))", "a");
        }

        [TestMethod]
        public void EvaluateNonSpreadLambdaTwoParametersAsList()
        {
            this.EvaluateAndCompare("((nlambda (x) x) 'a 'b)", "(a b)");
        }

        [TestMethod]
        public void EvaluateNonSpreadLambdaFirstOfTwoParametersAsList()
        {
            this.EvaluateAndCompare("((nlambda (x) (first x)) 'a 'b)", "a");
        }

        [TestMethod]
        public void EvaluateNonSpreadNonEvaluationLambdaTwoParameters()
        {
            this.EvaluateAndCompare("((flambda (x) x) a b)", "(a b)");
        }

        [TestMethod]
        public void EvaluateNonSpreadNonEvaluationLambdaFirstTwoParameters()
        {
            this.EvaluateAndCompare("((flambda (x) (first x)) a b)", "a");
        }

        [TestMethod]
        public void EvaluateMacroLambdaListFirstFirstRest()
        {
            this.EvaluateAndCompare("((mlambda (x) (list 'first (list 'quote x))) (a))", "a");
        }

        [TestMethod]
        public void EvaluateMacroLambdaListRestFirstRest()
        {
            this.EvaluateAndCompare("((mlambda (x) (list 'rest (list 'quote x))) (a b))", "(b)");
        }

        [TestMethod]
        public void EvaluateDefineFirst()
        {
            Machine machine = new Machine();
            machine.Evaluate("(define firstone (x) (first x))");
            this.EvaluateAndCompare(machine, "(firstone '(a b))", "a");
        }

        [TestMethod]
        public void EvaluateDefineRest()
        {
            Machine machine = new Machine();
            machine.Evaluate("(define restone (x) (rest x))");
            this.EvaluateAndCompare(machine, "(restone '(a b))", "(b)");
        }

        [TestMethod]
        public void EvaluateDefineNonSpreadList()
        {
            Machine machine = new Machine();
            machine.Evaluate("(definen list2 (x) x)");
            this.EvaluateAndCompare(machine, "(list2 'a 'b)", "(a b)");
        }

        [TestMethod]
        public void EvaluateDefineNonSpreadRest()
        {
            Machine machine = new Machine();
            machine.Evaluate("(definen rest2 (x) (rest x))");
            this.EvaluateAndCompare(machine, "(rest2 'a 'b)", "(b)");
        }

        [TestMethod]
        public void EvaluateDefineFList()
        {
            Machine machine = new Machine();
            machine.Evaluate("(definef list2 (x) x)");
            this.EvaluateAndCompare(machine, "(list2 a b)", "(a b)");
        }

        [TestMethod]
        public void EvaluateDefineFRest()
        {
            Machine machine = new Machine();
            machine.Evaluate("(definef rest2 (x) (rest x))");
            this.EvaluateAndCompare(machine, "(rest2 a b)", "(b)");
        }

        [TestMethod]
        public void EvaluateDefineFChoose()
        {
            Machine machine = new Machine();
            machine.Evaluate("(definef choose (args) (if (eval (car args)) (eval (car (cdr args))) (eval (car (cdr (cdr args))))))");
            this.EvaluateAndCompare(machine, "(choose t 'a 'b)", "a");
            this.EvaluateAndCompare(machine, "(choose nil 'a 'b)", "b");
        }

        [TestMethod]
        public void EvaluateSetAtom()
        {
            this.EvaluateAndCompare("(set 'a 'b)", "b");
        }

        [TestMethod]
        public void EvaluateSetAtomAndEvaluate()
        {
            Machine machine = new Machine();
            this.EvaluateAndCompare(machine, "(set 'a 'b)", "b");
            this.EvaluateAndCompare(machine, "a", "b");
        }

        [TestMethod]
        public void EvaluateLetAndEvaluate()
        {
            this.EvaluateAndCompare("(let ((a 1) (b 2)) (plus a b))", "3");
        }

        [TestMethod]
        public void EvaluateLetSAndEvaluate()
        {
            this.EvaluateAndCompare("(lets ((a 1) (b a)) (plus a b))", "2");
        }

        [TestMethod]
        public void EvaluateCondNil()
        {
            this.EvaluateAndCompare("(cond)", "nil");
        }

        [TestMethod]
        public void EvaluateCondTrue()
        {
            this.EvaluateAndCompare("(cond (t 1))", "1");
        }

        [TestMethod]
        public void EvaluateCondTrueMultiple()
        {
            this.EvaluateAndCompare("(cond (t 1 2))", "2");
        }

        [TestMethod]
        public void EvaluateCondNilTrue()
        {
            this.EvaluateAndCompare("(cond (nil 0) (t 1))", "1");
        }

        [TestMethod]
        public void EvaluateCondExpressions()
        {
            this.EvaluateAndCompare("(cond ((greater 3 1) 3) ((less 3 1) 1))", "3");
        }

        [TestMethod]
        public void EvaluateIfNil()
        {
            this.EvaluateAndCompare("(if nil 'a 'b)", "b");
        }

        [TestMethod]
        public void EvaluateIfTrue()
        {
            this.EvaluateAndCompare("(if t 'a 'b)", "a");
        }

        [TestMethod]
        public void EvaluateAppendListNil()
        {
            this.EvaluateAndCompare("(append '(a) nil)", "(a)");
        }

        [TestMethod]
        public void EvaluateAppendNilList()
        {
            this.EvaluateAndCompare("(append nil '(b))", "(b)");
        }

        [TestMethod]
        public void EvaluateAppendTwoSimpleLists()
        {
            this.EvaluateAndCompare("(append '(a) '(b))", "(a b)");
        }

        [TestMethod]
        public void EvaluateAppendListSimpleList()
        {
            this.EvaluateAndCompare("(append '(a b) '(c))", "(a b c)");
        }

        [TestMethod]
        public void EvaluateAppendTwoLists()
        {
            this.EvaluateAndCompare("(append '(a b) '(c d))", "(a b c d)");
        }

        [TestMethod]
        public void EvaluateAppendComplexLists()
        {
            this.EvaluateAndCompare("(append '((a1 a2) b) '((c1 c2) d))", "((a1 a2) b (c1 c2) d)");
        }

        [TestMethod]
        public void EvaluatePlusOneTwo()
        {
            this.EvaluateAndCompare("(plus 1 2)", "3");
        }

        [TestMethod]
        public void EvaluatePlusReals()
        {
            this.EvaluateAndCompare("(plus 1.2 2.3)", "3.5");
        }

        [TestMethod]
        public void EvaluatePlusOneTwoThree()
        {
            this.EvaluateAndCompare("(plus 1 2 3)", "6");
        }

        [TestMethod]
        public void EvaluateDifferenceOneTwo()
        {
            this.EvaluateAndCompare("(difference 1 2)", "-1");
        }

        [TestMethod]
        public void EvaluateDifferenceThreeTwo()
        {
            this.EvaluateAndCompare("(difference 3 2)", "1");
        }

        [TestMethod]
        public void EvaluateTimesOneTwo()
        {
            this.EvaluateAndCompare("(times 1 2)", "2");
        }

        [TestMethod]
        public void EvaluateTimesOneTwoThree()
        {
            this.EvaluateAndCompare("(times 1 2 3)", "6");
        }

        [TestMethod]
        public void EvaluateQuotientSixThree()
        {
            this.EvaluateAndCompare("(quotient 6 3)", "2");
        }

        [TestMethod]
        public void EvaluateQuotientTwoOne()
        {
            this.EvaluateAndCompare("(quotient 2 1)", "2");
        }

        [TestMethod]
        public void EvaluateRemainderSixThree()
        {
            this.EvaluateAndCompare("(remainder 6 3)", "0");
        }

        [TestMethod]
        public void EvaluateRemainderFiveThree()
        {
            this.EvaluateAndCompare("(remainder 5 3)", "2");
        }

        [TestMethod]
        public void EvaluateSimpleObjectProperty()
        {
            this.EvaluateAndCompare("(invoke \"foo\" \"Length\")", "3");
        }

        [TestMethod]
        public void EvaluateNewSimpleObject()
        {
            object result = this.Evaluate("(new System.Data.DataSet)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.Data.DataSet));
        }

        [TestMethod]
        public void EvaluateNewSimpleObjectWithArgument()
        {
            object result = this.Evaluate("(new \"System.IO.FileInfo\" \"myfile.txt\")");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.FileInfo));

            System.IO.FileInfo fi = (System.IO.FileInfo)result;

            Assert.AreEqual(fi.Name, "myfile.txt");
        }

        [TestMethod]
        public void EvaluateTypeMethodWithArgument()
        {
            object result = this.Evaluate("(type-invoke System.IO.File \"Exists\" \"nofile.txt\")");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse((bool)result);
        }

        [TestMethod]
        public void EvaluateBackquotedSimpleForm()
        {
            Machine machine = new Machine();
            machine.Environment.SetValue("x", 2);
            object expression = machine.Evaluate("`(1 ,x 3)");
            Assert.AreEqual("(1 2 3)", expression.ToString());
        }

        [TestMethod]
        public void EvaluateBackquotedForm()
        {
            Machine machine = new Machine();
            machine.Environment.SetValue("x", new List(2, new List(3)));
            object expression = machine.Evaluate("`(1 ,@x 4)");
            Assert.AreEqual("(1 2 3 4)", expression.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfUnknownForm()
        {
            this.Evaluate("(unknown 1 2)");
        }

        [TestMethod]
        public void EvaluateSimpleDefine()
        {
            Machine machine = new Machine();
            object result = machine.Evaluate("(define one 1)");
            Assert.AreEqual(1, result);
            Assert.AreEqual(1, machine.Evaluate("one"));
        }

        private object Evaluate(string text)
        {
            Machine machine = new Machine();
            return machine.Evaluate(text);
        }

        private void EvaluateAndCompare(string text, string expected)
        {
            Machine machine = new Machine();
            object expression = machine.Evaluate(text);
            Assert.AreEqual(expected, Conversions.ToPrintString(expression));
        }

        private void EvaluateAndCompare(Machine machine, string text, string expected)
        {
            object expression = machine.Evaluate(text);
            Assert.AreEqual(expected, expression.ToString());
        }
    }
}
