namespace AjLisp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLisp.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            this.EvaluateAndCompare("true", "true");
        }

        [TestMethod]
        public void EvaluateFalse()
        {
            this.EvaluateAndCompare("false", "false");
        }

        [TestMethod]
        public void EvaluateNilPredicate()
        {
            this.EvaluateAndCompare("(nil? nil)", "true");
            this.EvaluateAndCompare("(nil? true)", "false");
        }

        [TestMethod]
        public void EvaluateAtomNil()
        {
            this.EvaluateAndCompare("(atom? nil)", "true");
        }

        [TestMethod]
        public void EvaluateAtomTrue()
        {
            this.EvaluateAndCompare("(atom? true)", "true");
        }

        [TestMethod]
        public void EvaluateAtomFalse()
        {
            this.EvaluateAndCompare("(atom? false)", "true");
        }

        [TestMethod]
        public void EvaluateAtomQuotedAtom()
        {
            this.EvaluateAndCompare("(atom? 'a)", "true");
        }

        [TestMethod]
        public void EvaluateAtomInteger()
        {
            this.EvaluateAndCompare("(atom? 12)", "true");
        }

        [TestMethod]
        public void EvaluateAtomQuotedList()
        {
            this.EvaluateAndCompare("(atom? '(a b))", "false");
        }

        [TestMethod]
        public void EvaluateAtomEmptyList()
        {
            this.EvaluateAndCompare("(atom? ())", "true");
        }

        [TestMethod]
        public void EvaluateNilPredicateNil()
        {
            this.EvaluateAndCompare("(nil? nil)", "true");
        }

        [TestMethod]
        public void EvaluateNilPredicateTrue()
        {
            this.EvaluateAndCompare("(nil? true)", "false");
        }

        [TestMethod]
        public void EvaluateNilPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(nil? 'a)", "false");
        }

        [TestMethod]
        public void EvaluateNilPredicateQuotedList()
        {
            this.EvaluateAndCompare("(nil? '(a b))", "false");
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
            this.EvaluateAndCompare("(eval true)", "true");
        }

        [TestMethod]
        public void EvaluateEvalInteger()
        {
            this.EvaluateAndCompare("(eval 1)", "1");
        }

        [TestMethod]
        public void EvaluateEvalQuotedPlusTwoIntegers()
        {
            this.EvaluateAndCompare("(eval '(+ 1 2))", "3");
        }

        [TestMethod]
        public void EvaluateEvalQuotedAtomQuotedAtom()
        {
            this.EvaluateAndCompare("(eval '(atom? 'a))", "true");
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
        public void EvaluateFirstList()
        {
            this.EvaluateAndCompare("(first (list 'a 'b))", "a");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseOnEvaluateFirstQuotedAtom()
        {
            this.EvaluateAndCompare("(first 'a)", "a");
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
            this.EvaluateAndCompare("(cons? nil)", "false");
        }

        [TestMethod]
        public void EvaluateConsPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(cons? 'a)", "false");
        }

        [TestMethod]
        public void EvaluateConsPredicateQuotedList()
        {
            this.EvaluateAndCompare("(cons? '(a))", "true");
        }

        [TestMethod]
        public void EvaluateConsPredicateCons()
        {
            this.EvaluateAndCompare("(cons? (cons 'a '(b)))", "true");
        }

        [TestMethod]
        public void EvaluateIdPredicateNil()
        {
            this.EvaluateAndCompare("(id? nil)", "true");
        }

        [TestMethod]
        public void EvaluateIdPredicateTrue()
        {
            this.EvaluateAndCompare("(id? true)", "true");
        }

        [TestMethod]
        public void EvaluateIdPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(id? 'a)", "true");
        }

        [TestMethod]
        public void EvaluateIdPredicateQuotedList()
        {
            this.EvaluateAndCompare("(id? '(a b))", "false");
        }

        [TestMethod]
        public void EvaluateIdPredicateQuotedInteger()
        {
            this.EvaluateAndCompare("(id? 1)", "false");
        }

        [TestMethod]
        public void EvaluateIdPredicatePlus()
        {
            this.EvaluateAndCompare("(id? (+ 1 2))", "false");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateNil()
        {
            this.EvaluateAndCompare("(function? nil)", "false");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(function? 'a)", "false");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateAtom()
        {
            this.EvaluateAndCompare("(function? atom?)", "true");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateLambda()
        {
            this.EvaluateAndCompare("(function? (lambda (x) x))", "true");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateIf()
        {
            this.EvaluateAndCompare("(function? if)", "true");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateLambdaAtom()
        {
            this.EvaluateAndCompare("(function? lambda)", "true");
        }

        [TestMethod]
        public void EvaluateFunctionPredicateMacroLambda()
        {
            this.EvaluateAndCompare("(function? mlambda)", "true");
        }

        [TestMethod]
        public void EvaluateNumberPredicateNil()
        {
            this.EvaluateAndCompare("(number? nil)", "false");
        }

        [TestMethod]
        public void EvaluateNumberPredicateTrue()
        {
            this.EvaluateAndCompare("(number? t)", "false");
        }

        [TestMethod]
        public void EvaluateNumberPredicateQuotedAtom()
        {
            this.EvaluateAndCompare("(number? 'a)", "false");
        }

        [TestMethod]
        public void EvaluateNumberPredicateQuotedList()
        {
            this.EvaluateAndCompare("(number? '(a b))", "false");
        }

        [TestMethod]
        public void EvaluateNumberPredicateInteger()
        {
            this.EvaluateAndCompare("(number? 1)", "true");
        }

        [TestMethod]
        public void EvaluateNumberPredicatePlus()
        {
            this.EvaluateAndCompare("(number? (+ 1 2))", "true");
        }

        [TestMethod]
        public void EvaluateEqPredicateNilNil()
        {
            this.EvaluateAndCompare("(== nil nil)", "true");
        }

        [TestMethod]
        public void EvaluateEqPredicateTrueTrue()
        {
            this.EvaluateAndCompare("(== t t)", "true");
        }

        [TestMethod]
        public void EvaluateEqPredicateQuotedAtoms()
        {
            this.EvaluateAndCompare("(== 'a 'a)", "true");
        }

        [TestMethod]
        public void EvaluateEqPredicateQuotedDifferentAtoms()
        {
            this.EvaluateAndCompare("(== 'a 'b)", "false");
        }

        [TestMethod]
        public void EvaluateEqPredicateIntegers()
        {
            this.EvaluateAndCompare("(== 12 20)", "false");
        }

        [TestMethod]
        public void EvaluateEqPredicateOneOne()
        {
            this.EvaluateAndCompare("(== 1 1)", "true");
        }

        [TestMethod]
        public void EvaluateEqPredicateIntegerPlus()
        {
            this.EvaluateAndCompare("(== 3 (+ 1 2))", "true");
        }

        [TestMethod]
        public void EvaluateGreaterPredicateTwoOne()
        {
            this.EvaluateAndCompare("(> 2 1)", "true");
        }

        [TestMethod]
        public void EvaluateGreaterPredicateOneTwo()
        {
            this.EvaluateAndCompare("(> 1 2)", "false");
        }

        [TestMethod]
        public void EvaluateGreaterPredicateAtomsAB()
        {
            this.EvaluateAndCompare("(> 'a 'b)", "false");
        }

        [TestMethod]
        public void EvaluateGreaterPredicateAtomsBA()
        {
            this.EvaluateAndCompare("(> 'b 'a)", "true");
        }

        [TestMethod]
        public void EvaluateGreaterEqualPredicateOneTwo()
        {
            this.EvaluateAndCompare("(>= 1 2)", "false");
        }

        [TestMethod]
        public void EvaluateGreaterEqualPredicateAtomsAB()
        {
            this.EvaluateAndCompare("(>= 'a 'b)", "false");
        }

        [TestMethod]
        public void EvaluateGreaterEqualPredicateAtomsBA()
        {
            this.EvaluateAndCompare("(>= 'b 'a)", "true");
        }

        [TestMethod]
        public void EvaluateGreaterEqualPredicateAtomsAA()
        {
            this.EvaluateAndCompare("(>= 'a 'a)", "true");
        }

        [TestMethod]
        public void EvaluateLessPredicateTwoOne()
        {
            this.EvaluateAndCompare("(< 2 1)", "false");
        }

        [TestMethod]
        public void EvaluateLessPredicateOneTwo()
        {
            this.EvaluateAndCompare("(< 1 2)", "true");
        }

        [TestMethod]
        public void EvaluateLessPredicateAtomsAB()
        {
            this.EvaluateAndCompare("(< 'a 'b)", "true");
        }

        [TestMethod]
        public void EvaluateLessPredicateAtomsBA()
        {
            this.EvaluateAndCompare("(< 'b 'a)", "false");
        }

        [TestMethod]
        public void EvaluateLessEqualPredicateTwoOne()
        {
            this.EvaluateAndCompare("(<= 2 1)", "false");
        }

        [TestMethod]
        public void EvaluateLessEqualPredicateOneTwo()
        {
            this.EvaluateAndCompare("(<= 1 2)", "true");
        }

        [TestMethod]
        public void EvaluateLessEqualPredicateAtomsAB()
        {
            this.EvaluateAndCompare("(<= 'a 'b)", "true");
        }

        [TestMethod]
        public void EvaluateLessEqualPredicateAtomsBA()
        {
            this.EvaluateAndCompare("(<= 'b 'a)", "false");
        }

        [TestMethod]
        public void EvaluateLessEqualPredicateAtomsAA()
        {
            this.EvaluateAndCompare("(<= 'a 'a)", "true");
        }

        [TestMethod]
        public void EvaluateProgNTrue()
        {
            this.EvaluateAndCompare("(progn true)", "true");
        }

        [TestMethod]
        public void EvaluateProgNNil()
        {
            this.EvaluateAndCompare("(progn nil)", "nil");
        }

        [TestMethod]
        public void EvaluateProgNFalse()
        {
            this.EvaluateAndCompare("(progn false)", "false");
        }

        [TestMethod]
        public void EvaluateProgNAtoms()
        {
            this.EvaluateAndCompare("(progn 'a 'b)", "b");
        }

        [TestMethod]
        public void EvaluateProgNTwoExpressions()
        {
            this.EvaluateAndCompare("(progn (+ 1 2) (+ 3 4))", "7");
        }

        [TestMethod]
        public void EvaluateProgNThreeExpressions()
        {
            this.EvaluateAndCompare("(progn (+ 1 2) (+ 3 4) (+ 5 6))", "11");
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
            machine.Evaluate("(definef choose (args) (if (eval (first args)) (eval (first (rest args))) (eval (first (rest (rest args))))))");
            this.EvaluateAndCompare(machine, "(choose true 'a 'b)", "a");
            this.EvaluateAndCompare(machine, "(choose nil 'a 'b)", "b");
            this.EvaluateAndCompare(machine, "(choose false 'a 'b)", "b");
        }

        [TestMethod]
        public void EvaluateDefineMMyMacro()
        {
            Machine machine = new Machine();
            machine.Evaluate("(definem mymacro (x) (list 'first (list 'quote x)))");
            this.EvaluateAndCompare(machine, "(mymacro (a))", "a");
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
            this.EvaluateAndCompare("(let ((a 1) (b 2)) (+ a b))", "3");
        }

        [TestMethod]
        public void EvaluateLetSAndEvaluate()
        {
            this.EvaluateAndCompare("(lets ((a 1) (b a)) (+ a b))", "2");
        }

        [TestMethod]
        public void EvaluateCondNil()
        {
            this.EvaluateAndCompare("(cond)", "nil");
        }

        [TestMethod]
        public void EvaluateCondTrue()
        {
            this.EvaluateAndCompare("(cond (true 1))", "1");
        }

        [TestMethod]
        public void EvaluateCondTrueMultiple()
        {
            this.EvaluateAndCompare("(cond (true 1 2))", "2");
        }

        [TestMethod]
        public void EvaluateCondNilTrue()
        {
            this.EvaluateAndCompare("(cond (nil 0) (true 1))", "1");
        }

        [TestMethod]
        public void EvaluateCondFalseTrue()
        {
            this.EvaluateAndCompare("(cond (false 0) (true 1))", "1");
        }

        [TestMethod]
        public void EvaluateCondExpressions()
        {
            this.EvaluateAndCompare("(cond ((> 3 1) 3) ((< 3 1) 1))", "3");
        }

        [TestMethod]
        public void EvaluateIfNil()
        {
            this.EvaluateAndCompare("(if nil 'a 'b)", "b");
        }

        [TestMethod]
        public void EvaluateIfFalse()
        {
            this.EvaluateAndCompare("(if false 'a 'b)", "b");
        }

        [TestMethod]
        public void EvaluateIfTrue()
        {
            this.EvaluateAndCompare("(if true 'a 'b)", "a");
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
            this.EvaluateAndCompare("(+ 1 2)", "3");
        }

        [TestMethod]
        public void EvaluatePlusReals()
        {
            this.EvaluateAndCompare("(+ 1.2 2.3)", "3.5");
        }

        [TestMethod]
        public void EvaluatePlusOneTwoThree()
        {
            this.EvaluateAndCompare("(+ 1 2 3)", "6");
        }

        [TestMethod]
        public void EvaluateDifferenceOneTwo()
        {
            this.EvaluateAndCompare("(- 1 2)", "-1");
        }

        [TestMethod]
        public void EvaluateDifferenceThreeTwo()
        {
            this.EvaluateAndCompare("(- 3 2)", "1");
        }

        [TestMethod]
        public void EvaluateTimesOneTwo()
        {
            this.EvaluateAndCompare("(* 1 2)", "2");
        }

        [TestMethod]
        public void EvaluateTimesOneTwoThree()
        {
            this.EvaluateAndCompare("(* 1 2 3)", "6");
        }

        [TestMethod]
        public void EvaluateQuotientSixThree()
        {
            this.EvaluateAndCompare("(/ 6 3)", "2");
        }

        [TestMethod]
        public void EvaluateQuotientTwoOne()
        {
            this.EvaluateAndCompare("(/ 2 1)", "2");
        }

        [TestMethod]
        public void EvaluateRemainderSixThree()
        {
            this.EvaluateAndCompare("(% 6 3)", "0");
        }

        [TestMethod]
        public void EvaluateRemainderFiveThree()
        {
            this.EvaluateAndCompare("(% 5 3)", "2");
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

        [TestMethod]
        public void EvaluateNewDataSet()
        {
            var result = this.Evaluate("(System.Data.DataSet.)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.Data.DataSet));
        }

        [TestMethod]
        public void EvaluateNewFileInfo()
        {
            var result = this.Evaluate("(System.IO.FileInfo. \"foo.txt\")");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.FileInfo));
        }

        [TestMethod]
        public void EvaluateProperty()
        {
            var result = this.Evaluate("(.Length \"foo\")");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void EvaluateMethod()
        {
            var result = this.Evaluate("(.ToString 123)");

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result);
        }

        [TestMethod]
        public void EvaluateMethodWithArguments()
        {
            var result = this.Evaluate("(.Substring \"foobarfoo\" 3 3)");

            Assert.IsNotNull(result);
            Assert.AreEqual("bar", result);
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
