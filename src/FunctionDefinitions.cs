namespace RAScriptLanguageServer
{
    public class FunctionDefinitions
    {
        public readonly FunctionDefinition[] functionDefinitions;
        public FunctionDefinitions()
        {
            this.functionDefinitions = new FunctionDefinition[] { 
                new FunctionDefinition() {
                    Key = "byte",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 8-bit value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "word",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 16-bit value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "tbyte",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 24-bit value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "dword",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 32-bit value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit0",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the least significant bit of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit1",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the second least significant bit of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit2",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the third least significant bit of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit3",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the fourth least significant bit of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit4",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the fifth least significant bit of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit5",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the sixth least significant bit of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit6",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the seventh least significant bit of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit7",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the most significant bit of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bit",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "index",
                        "address"
                    ],
                    CommentDoc = [
                        "// the `index`th bit of the specified address (`index` must be between 0 and 31)"
                    ]
                },
                new FunctionDefinition() {
                    Key = "low4",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the four least significant bits of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "high4",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the four most significant bits of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bitcount",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the number of non-zero bits at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "word_be",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 16-bit big-endian value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "tbyte_be",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 24-bit big-endian value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "dword_be",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 32-bit big-endian value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "float",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 32-bit IEEE-754 floating point value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "float_be",
                    URL = "",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// Undocumented"
                    ]
                },
                new FunctionDefinition() {
                    Key = "mbf32",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 32-bit Microsoft Binary Format floating point value at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "mbf32_le",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 32-bit Microsoft Binary Format floating point value (in little-endian form) at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "double32",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 32 most significant bits of a 64-bit double at the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "double32_be",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "address"
                    ],
                    CommentDoc = [
                        "// the 32 most significant bits of a 64-bit double (in big-endian form).",
                        "// Note: specified address should be offset for the most significant bits."
                    ]
                },
                new FunctionDefinition() {
                    Key = "prev",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "accessor"
                    ],
                    CommentDoc = [
                        "// the value of the specified address from the previous frame"
                    ]
                },
                new FunctionDefinition() {
                    Key = "prior",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "accessor"
                    ],
                    CommentDoc = [
                        "// the last differing value of the specified address"
                    ]
                },
                new FunctionDefinition() {
                    Key = "bcd",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "accessor"
                    ],
                    CommentDoc = [
                        "// converts a BCD-encoded value to decimal for leaderboard and rich presence values."
                    ]
                },
                new FunctionDefinition() {
                    Key = "identity_transform",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory",
                    Args = [
                        "accessor"
                    ],
                    CommentDoc = [
                        "// Undocumented"
                    ]
                },
                new FunctionDefinition() {
                    Key = "ascii_string_equals",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory#string-matching",
                    Args = [
                        "address",
                        "string",
                        "length",
                        "transform"
                    ],
                    CommentDoc = [
                        "// match memory to strings assuming they're encoded using ASCII (7-bit latin characters)",
                        "// ",
                        "// Matches `length` characters starting at `address`.",
                        "// If `length` is unspecified, the length of the string is used.",
                        "// ",
                        "// `transform` allows the caller to wrap the generated logic in `prev` or `prior` (i.e. `transform = a => prev(a)`).",
                        "// The expression passed to the `transform` function may be multiple bytes (i.e. `dword(address)`) as the comparison is broken into chunks.",
                        "// ",
                        "// ### Examples",
                        "// ",
                        "// | Example | Returns | Notes |",
                        "// | ------- | ------- | ----- |",
                        "// | `ascii_string_equals(0x1234, \"Hi\")` | word(0x1234) == 0x6948 | Implicit length of string (2) used |",
                        "// | `ascii_string_equals(0x1234, \"World\", 6)` | dword(0x1234) == 0x6c726f57 && word(0x1238) == 0x0064 | Explicit length includes null terminator |",
                        "// | `ascii_string_equals(0x1234, \"World\", 6, transform = a => prev(a))` | prev(dword(0x1234)) == 0x6c726f57 && prev(word(0x1238)) == 0x0064 | `transform` wraps each chunk |",
                        "// | `ascii_string_equals(0x1234, \"World\", 3)` | tbyte(0x1234) == 0x726f57 | Explicit length only checks part of the string |",
                        "// | `unicode_string_equals(0x1234, \"Hi\")` | dword(0x1234) == 0x00690048 | Unicode characters are 16-bits each |"
                    ]
                },
                new FunctionDefinition() {
                    Key = "unicode_string_equals",
                    URL = "https://github.com/Jamiras/RATools/wiki/Accessing-Memory#string-matching",
                    Args = [
                        "address",
                        "string",
                        "length",
                        "transform"
                    ],
                    CommentDoc = [
                        "// match memory to strings assuming they're encoded using Unicode (16-bit international alphabets)",
                        "// ",
                        "// Matches `length` characters starting at `address`.",
                        "// If `length` is unspecified, the length of the string is used.",
                        "// ",
                        "// `transform` allows the caller to wrap the generated logic in `prev` or `prior` (i.e. `transform = a => prev(a)`).",
                        "// The expression passed to the `transform` function may be multiple bytes (i.e. `dword(address)`) as the comparison is broken into chunks.",
                        "// ",
                        "// ### Examples",
                        "// ",
                        "// | Example | Returns | Notes |",
                        "// | ------- | ------- | ----- |",
                        "// | `ascii_string_equals(0x1234, \"Hi\")` | word(0x1234) == 0x6948 | Implicit length of string (2) used |",
                        "// | `ascii_string_equals(0x1234, \"World\", 6)` | dword(0x1234) == 0x6c726f57 && word(0x1238) == 0x0064 | Explicit length includes null terminator |",
                        "// | `ascii_string_equals(0x1234, \"World\", 6, transform= a=>prev(a))` | prev(dword(0x1234)) == 0x6c726f57 && prev(word(0x1238)) == 0x0064 | `transform` wraps each chunk |",
                        "// | `ascii_string_equals(0x1234, \"World\", 3)` | tbyte(0x1234) == 0x726f57 | Explicit length only checks part of the string |",
                        "// | `unicode_string_equals(0x1234, \"Hi\")` | dword(0x1234) == 0x00690048 | Unicode characters are 16-bits each |"
                    ]
                },
                new FunctionDefinition() {
                    Key = "repeated",
                    URL = "https://github.com/Jamiras/RATools/wiki/Trigger-Functions#repeatedcount-comparison",
                    Args = [
                        "count",
                        "comparison"
                    ],
                    CommentDoc = [
                        "// Adds a Hit Target to the condition.",
                        "// The specified `comparison` must be true for `count` frames for the trigger to fire.",
                        "// The frames do not have to be consecutive.",
                        "// Once the Hit Target is met, the condition is considered true until it is reset."
                    ]
                },
                new FunctionDefinition() {
                    Key = "once",
                    URL = "https://github.com/Jamiras/RATools/wiki/Trigger-Functions#oncecomparison",
                    Args = [
                        "comparison"
                    ],
                    CommentDoc = [
                        "// Shorthand for",
                        "// ",
                        "// ```rascript",
                        "// repeated(1, comparison)",
                        "// ```",
                        "// ",
                        "//The specified `comparison` must have been true at one point, but is not required to currently be true to trigger the achievement."
                    ]
                },
                new FunctionDefinition() {
                    Key = "tally",
                    URL = "https://github.com/Jamiras/RATools/wiki/Trigger-Functions#tallycount-comparisons",
                    Args = [
                        "count",
                        "comparisons"
                    ],
                    CommentDoc = [
                        "// Adds a Hit Target to the condition where multiple conditions may be true in the same frame.",
                        "// `comparison` may be an array of conditions, or multiple conditions passed as individual parameters.",
                        "// Each condition that is true on each frame will tally a Hit Count.",
                        "// Multiple Hit Counts may be tallied in the same frame.",
                        "// The overall tally must reach `count` for the trigger to fire.",
                        "// Once the Hit Target is met, the condition is considered true until it is reset.",
                        "// ",
                        "// Individual conditions in the `comparisons` list may be wrapped in a `deduct()` function call, which causes any hits counted by the condition to be deducted from the overall tally.",
                        "// ",
                        "// If `count` is zero, the overall condition will become true as soon as any individual comparison is true.",
                        "// This is mostly used when building leaderboard value clauses using the `measured` function as it provides an unbounded counting of the subclauses."
                    ]
                },
                new FunctionDefinition() {
                    Key = "deduct",
                    URL = "",
                    Args = [
                        "comparison"
                    ],
                    CommentDoc = [
                        "// Undocumented"
                    ]
                },
                new FunctionDefinition() {
                    Key = "never",
                    URL = "https://github.com/Jamiras/RATools/wiki/Trigger-Functions#nevercomparison",
                    Args = [
                        "comparison"
                    ],
                    CommentDoc = [
                        "// This becomes a `ResetIf`.",
                        "// If the `comparison` is true, all Hit Counts in the trigger are reset to 0, and the trigger cannot fire."
                    ]
                },
                new FunctionDefinition() {
                    Key = "unless",
                    URL = "https://github.com/Jamiras/RATools/wiki/Trigger-Functions#unlesscomparison",
                    Args = [
                        "comparison"
                    ],
                    CommentDoc = [
                        "// This becomes a `PauseIf`.",
                        "// The group containing the `PauseIf` is not processed while the `condition` is true, and the trigger cannot fire.",
                        "// ",
                        "// `unless` has precedence over `never`.",
                        "// A paused group will not evaluate it's reset statements.",
                        "// If the `comparison` is a `repeated` condition, once the Hit Target has been met, the group will be \"Pause Lock\"ed until a `never` resets it's Hit Count from _another_ group.",
                        "// See also [Runtime Processing Notes](https://github.com/Jamiras/RATools/wiki/Runtime-Processing-Notes)."
                    ]
                },
                new FunctionDefinition() {
                    Key = "measured",
                    URL = "https://github.com/Jamiras/RATools/wiki/Trigger-Functions#measuredcomparison-whenalways_true-formatraw",
                    Args = [
                        "comparison",
                        "when",
                        "format"
                    ],
                    CommentDoc = [
                        "// This adds a `Measured` flag to the comparison.",
                        "// If the `comparison` is `repeated`, the Measured value will be the current number of hits on the condition, and the measurement target will be the Hit Target for the condition.",
                        "// Otherwise, the Measured value will be the left side value and the measurement target will be the right side value (regardless of the comparison operation).",
                        "// ",
                        "// When used in an [achievement](https://github.com/Jamiras/RATools/wiki/Achievement-Functions), Measurements are displayed in the overlay.",
                        "// Use the `when` parameter to specify a secondary condition that must be true for the Measured value to be reported (i.e. for achievements where the player must be using a specific character).",
                        "// If the `when` condition is false, the Measured value will be 0, regardless of the values in the associated memory.",
                        "// Both the `comparison` (and `when` condition if provided) must be true for the achievement to trigger.",
                        "// ",
                        "// `format` may be set to `percent` to change the display in the overlay to report a percentage instead of the raw measured value (i.e. 75% instead of 3/4)",
                        "// ",
                        "// When used in [rich presence](https://github.com/Jamiras/RATools/wiki/Rich-Presence-Functions) or [leaderboards](https://github.com/Jamiras/RATools/wiki/Leaderboard-Functions), the Measured value is captured and the measurement target is ignored.",
                        "// ",
                        "// #### Using with complex conditions",
                        "// ",
                        "// `comparison` may be a series of AND'd or OR'd conditions.",
                        "// This will cause `repeated`, `once`, and `measured` to generate a series of OrNext and AndNext conditions, and `never` and `unless` will generate a series of ResetIf/PauseIf conditions."
                    ]
                },
                new FunctionDefinition() {
                    Key = "trigger_when",
                    URL = "https://github.com/Jamiras/RATools/wiki/Trigger-Functions#trigger_whencomparison",
                    Args = [
                        "comparison"
                    ],
                    CommentDoc = [
                        "// This adds a `Trigger` flag to the comparison, which tells the runtime that the specified conditions are the last conditions that will be true for the achievement.",
                        "// When all other logical conditions are true, the runtime may display an indicator on-screen to let the user know they're close to completing an achievement.",
                        "// Should be used for tracking challenges, like defeating a boss without taking damage."
                    ]
                },
                new FunctionDefinition() {
                    Key = "disable_when",
                    URL = "https://github.com/Jamiras/RATools/wiki/Trigger-Functions#disable_whencomparison-untilalways_false",
                    Args = [
                        "comparison",
                        "until"
                    ],
                    CommentDoc = [
                        "// This adds a `PauseIf` flag and a hit target to the comparison.",
                        "// If `comparison` is not a `repeated()` condition, the hit target will be 1, otherwise the hit target will come from the `repeated()` function call.",
                        "// When the hit target is met, the runtime will disable the achievement indefinitely.",
                        "// This is most often used to disable achievements while a cheat is active.",
                        "// ",
                        "// If `until` is specified, it will generate a `ResetNextIf` condition attached to the `PauseIf`, which will clear the hit count when true, thereby re-enabling the achievement."
                    ]
                },
                new FunctionDefinition() {
                    Key = "always_true",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#always_true",
                    Args = [],
                    CommentDoc = [
                        "// Defines the clause \"1==1\".",
                        "// It is typically only used to move a PauseIf/ResetIf to an alt group:",
                        "// ",
                        "// ```rascript",
                        "// byte(0x1234) == 8 && (always_true() || (never(byte(0x2345) == 12) && unless(byte(0x3456) == 6)))",
                        "// ```",
                        "// ",
                        "// This allows the achievement (core group) to trigger while the `never` is paused by the `unless`."
                    ]
                },
                new FunctionDefinition() {
                    Key = "always_false",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#always_false",
                    Args = [],
                    CommentDoc = [
                        "// Defines the clause \"0==1\".",
                        "// It is typically used for constructing alt chains, as a variable must have an initial value:",
                        "// ",
                        "// ```rascript",
                        "// trigger = always_false()",
                        "// for test in tests",
                        "//     trigger = trigger || test",
                        "// achievement(..., trigger = trigger)",
                        "// ```",
                        "// ",
                        "// If more than two alt groups exist, the `always_false` group will be removed from the final achievement code"
                    ]
                },
                new FunctionDefinition() {
                    Key = "format",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#formatformat_string-parameters",
                    Args = [
                        "format_string",
                        "parameters"
                    ],
                    CommentDoc = [
                        "// Builds a string by inserting `parameters` into placeholders in the `format_string`.",
                        "// ",
                        "// For example:",
                        "// ",
                        "// ```rascript",
                        "// stage_names = new FunctionDefinition() {",
                        "//     1: \"Downtown\"",
                        "// }",
                        "// stage_1_label = format(\"Stage new FunctionDefinition() {0} - new FunctionDefinition() {1}\", 1, stage_names[1])",
                        "// ```",
                        "// Would set `stage_1_label` to \"Stage 1 - Downtown\""
                    ]
                },
                new FunctionDefinition() {
                    Key = "substring",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#substringstring-offset-length0x7fffffff",
                    Args = [
                        "string",
                        "offset",
                        "length"
                    ],
                    CommentDoc = [
                        "// Returns `length` characters of `string`, starting at `offset` (0-based).",
                        "// A negative offset indicates the returned value should start that far from the end of the string.",
                        "// A negative length indicates the returned value should stop that far from the end of the string.",
                        "// ",
                        "// ```rascript",
                        "// bc  = substring(\"abcdef\", 1, 2)  // take two characters starting at index 1",
                        "// def = substring(\"abcdef\", 3)     // take all remaining characters starting at index 3",
                        "// e   = substring(\"abcdef\", -2, 1) // starting two from end, take one character",
                        "// cd  = substring(\"abcdef\", 2, -2) // starting at index 2, take all but two characters",
                        "// ```",
                        "// ",
                        "// If the `offset` or `length` parameters extend beyond the length of the string, only characters at indices within the string will be returned.",
                        "// If all captured characters would be outside the string, an empty string is returned."
                    ]
                },
                new FunctionDefinition() {
                    Key = "length",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#lengthobject",
                    Args = [
                        "object"
                    ],
                    CommentDoc = [
                        "// Returns the number of elements in a dictionary or array, or the number of characters in a string."
                    ]
                },
                new FunctionDefinition() {
                    Key = "range",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#rangestart-stop-step1",
                    Args = [
                        "start",
                        "stop",
                        "step"
                    ],
                    CommentDoc = [
                        "// Returns an array containing integers starting at `start` and continuing until `stop`.",
                        "// ",
                        "// If `step` is specified, the second item will be `start+step`, the third will be `start+step*2`, and so on until a value greater than `stop` would be generated.",
                        "// That value will be ignored."
                    ]
                },
                new FunctionDefinition() {
                    Key = "array_push",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#array_pusharray-value",
                    Args = [
                        "array",
                        "value"
                    ],
                    CommentDoc = [
                        "// Appends `value` to the end of `array`."
                    ]
                },
                new FunctionDefinition() {
                    Key = "array_pop",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#array_poparray",
                    Args = [
                        "array"
                    ],
                    CommentDoc = [
                        "// Removes and returns the last value from `array`.",
                        "// If `array` is empty, integer 0 is returned.",
                        "// You can use `length()` to determine if `array` is empty before calling `array_pop`."
                    ]
                },
                new FunctionDefinition() {
                    Key = "array_map",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#array_mapinputs-predicate",
                    Args = [
                        "inputs",
                        "predicate"
                    ],
                    CommentDoc = [
                        "// Returns an array generated by processing each item in `inputs` through `predicate`.",
                        "// ",
                        "// `inputs` is an array or a dictionary.",
                        "// If a dictionary is provided, the keys will be passed to the `predicate`.",
                        "// ",
                        "// `predicate` is a function that accepts a single input and returns an expression or constant constructed from that input.",
                        "// The expressions returned by `predicate` are stored in the new array that is returned by `array_map`."
                    ]
                },
                new FunctionDefinition() {
                    Key = "array_contains",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#array_containsarray-value",
                    Args = [
                        "array",
                        "value"
                    ],
                    CommentDoc = [
                        "// Returns `true` if `value` is found in `array`."
                    ]
                },
                new FunctionDefinition() {
                    Key = "array_reduce",
                    URL = "",
                    Args = [
                        "inputs",
                        "initial",
                        "reducer"
                    ],
                    CommentDoc = [
                        "// Undocumented"
                    ]
                },
                new FunctionDefinition() {
                    Key = "array_filter",
                    URL = "",
                    Args = [
                        "inputs",
                        "predicate"
                    ],
                    CommentDoc = [
                        "// Undocumented"
                    ]
                },
                new FunctionDefinition() {
                    Key = "dictionary_contains_key",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#dictionary_contains_keydictionary-key",
                    Args = [
                        "dictionary",
                        "key"
                    ],
                    CommentDoc = [
                        "// Returns `true` if `dictionary` contains an entry for the specified `key`."
                    ]
                },
                new FunctionDefinition() {
                    Key = "any_of",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#any_ofinputs-predicate",
                    Args = [
                        "inputs",
                        "predicate"
                    ],
                    CommentDoc = [
                        "// Returns an expression that will evaluate true if any of the `inputs` matches the `predicate`.",
                        "// ",
                        "// `inputs` is an array or a dictionary.",
                        "// If a dictionary is provided, the keys will be passed to the `predicate`.",
                        "// ",
                        "// `predicate` is a function that accepts a single input and returns an expression constructed from that input.",
                        "// The expressions returned by `predicate` are joined with `||`s."
                    ]
                },
                new FunctionDefinition() {
                    Key = "all_of",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#all_ofinputs-predicate",
                    Args = [
                        "inputs",
                        "predicate"
                    ],
                    CommentDoc = [
                        "// Returns an expression that will evaluate true if all of the `inputs` matches the `predicate`.",
                        "// ",
                        "// `inputs` is an array or a dictionary.",
                        "// If a dictionary is provided, the keys will be passed to the `predicate`.",
                        "// ",
                        "// `predicate` is a function that accepts a single input and returns an expression constructed from that input.",
                        "// The expressions returned by `predicate` are joined with `&&`s."
                    ]
                },
                new FunctionDefinition() {
                    Key = "none_of",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#none_ofinputs-predicate",
                    Args = [
                        "inputs",
                        "predicate"
                    ],
                    CommentDoc = [
                        "// Returns an expression that will evaluate true if all of the `inputs` do not match the `predicate`.",
                        "// ",
                        "// `inputs` is an array or a dictionary.",
                        "// If a dictionary is provided, the keys will be passed to the `predicate`.",
                        "// ",
                        "// `predicate` is a function that accepts a single input and returns an expression constructed from that input.",
                        "// The expressions returned by `predicate` are negated (`!`) and then joined with `&&`s."
                    ]
                },
                new FunctionDefinition() {
                    Key = "sum_of",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#sum_ofinputs-predicate",
                    Args = [
                        "inputs",
                        "predicate"
                    ],
                    CommentDoc = [
                        "// Returns an expression that will calculate the sum of the `inputs` modified by the `predicate`.",
                        "// ",
                        "// `inputs` is an array or a dictionary.",
                        "// If a dictionary is provided, the keys will be passed to the `predicate`.",
                        "// ",
                        "// `predicate` is a function that accepts a single input and returns an expression constructed from that input.",
                        "// The expressions returned by `predicate` are added together with `+`."
                    ]
                },
                new FunctionDefinition() {
                    Key = "tally_of",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#tally_ofinputs-count-predicate",
                    Args = [
                        "inputs",
                        "count",
                        "predicate"
                    ],
                    CommentDoc = [
                        "// Returns a [`tally`](https://github.com/Jamiras/RATools/wiki/Trigger-Functions#tallycount-comparisons) expression where each comparison is generated by running the `inputs` through the `predicate`.",
                        "// ",
                        "// `inputs` is an array or a dictionary.",
                        "// If a dictionary is provided, the keys will be passed to the `predicate`.",
                        "// ",
                        "// `predicate` is a function that accepts a single input and returns an expression constructed from that input.",
                        "// The expressions returned by `predicate` are used in the `tally` expression."
                    ]
                },
                new FunctionDefinition() {
                    Key = "max_of",
                    URL = "",
                    Args = [
                        "inputs"
                    ],
                    CommentDoc = [
                        "// Undocumented"
                    ]
                },
                new FunctionDefinition() {
                    Key = "assert",
                    URL = "https://github.com/Jamiras/RATools/wiki/Built-in-Functions#assertcondition-message",
                    Args = [
                        "condition",
                        "message"
                    ],
                    CommentDoc = [
                        "// Generates a processing error if `condition` is false.",
                        "// `message` defines what the error should say.",
                        "// If not specified, the message will be the `condition` as a string."
                    ]
                },
                new FunctionDefinition() {
                    Key = "achievement",
                    URL = "https://github.com/Jamiras/RATools/wiki/Achievement-Functions#achievementtitle-description-points-trigger-id0-published-modified-badge0-type-set0",
                    Args = [
                        "title",
                        "description",
                        "points",
                        "trigger",
                        "id",
                        "published",
                        "modified",
                        "badge",
                        "type",
                        "set"
                    ],
                    CommentDoc = [
                        "// Defines a new achievement with the specified `title` (string), `description` (string), `points` (integer), and `trigger`.",
                        "// ",
                        "// `trigger` is an expression with one or more [comparisons](https://github.com/Jamiras/RATools/wiki/Operators#comparisons).",
                        "// These may be modified by [trigger functions](https://github.com/Jamiras/RATools/wiki/Trigger-Functions). ",
                        "// ",
                        "// if `id` is provided when calling the `achievement` function, the script will generate a local achievement definition that the toolkit will merge into the existing achievement instead of putting as a separate local achievement.",
                        "// ",
                        "// `badge` can be used to specify the image associated to the achievement.",
                        "// The value is the ID of the badge (already on the server).",
                        "// RATools will not upload a badge for you.",
                        "// ",
                        "// `published` and `modified` are unused parameters that are populated when generating a script from existing achievements.",
                        "// ",
                        "// `type` specifies the classification of the achievement.",
                        "// Valid values are:",
                        "// ",
                        "// | value | type |",
                        "// | ---- | ---- |",
                        "// | `\"\"` | Standard (unflagged) |",
                        "// | `\"missable\"` | Missable |",
                        "// | `\"progression\"` | Progression |",
                        "// | `\"win_condition\"` | Win condition |",
                        "// ",
                        "// `set` specifies the unique identifier of the [achievement set](https://github.com/Jamiras/RATools/wiki/Achievement-Functions#achievement_settitle-typebonus-id0-game_id0) the achievement should be partitioned into.",
                        "// ",
                        "// #### Example",
                        "// ",
                        "// ```rascript",
                        "// function current_board() => byte(0x0088)",
                        "// function current_player() => byte(0x008C)",
                        "// function trigger_win_game() => current_board() == 0x8B && current_player() == 0",
                        "// ",
                        "// achievement(",
                        "//     title = \"Score!\", description = \"Win a game with at least 600 points\", points = 25,",
                        "//     trigger = trigger_win_game() && byte(0x0573) >= 0xF6 // 0x0573 is hundreds place of score",
                        "// )",
                        "// ```",
                        "// ",
                        "// First, a couple of helper functions are defined to make the code easier to read. ",
                        "// Because all functions are inlined and evaluated on use, this expands to:",
                        "// ",
                        "// ```rascript",
                        "// trigger = byte(0x0088) == 0x8B && byte(0x008C) == 0 && byte(0x0573) >= 0xF6",
                        "// ```"
                    ]
                },
                new FunctionDefinition() {
                    Key = "rich_presence_display",
                    URL = "https://github.com/Jamiras/RATools/wiki/Rich-Presence-Functions#rich_presence_displayformat_string-parameters",
                    Args = [
                        "format_string",
                        "parameters"
                    ],
                    CommentDoc = [
                        "// Defines the rich presence display string. Only one string may be defined per script.",
                        "// If this function is called multiple times, the last one will win.",
                        "// ",
                        "// `format_string` is a string with zero or more placeholders that will be evaluated by the emulator at runtime.",
                        "// It uses the same syntax as the [`format`](https://github.com/Jamiras/RATools/wiki/Built-in-Functions#formatformat_string-parameters) function.",
                        "// ",
                        "// For each placeholder a parameter must be defined using a `rich_presence_value` or `rich_presence_lookup` function."
                    ]
                },
                new FunctionDefinition() {
                    Key = "rich_presence_value",
                    URL = "https://github.com/Jamiras/RATools/wiki/Rich-Presence-Functions#rich_presence_valuename-expression-format",
                    Args = [
                        "name",
                        "expression",
                        "format"
                    ],
                    CommentDoc = [
                        "// `name` is the name to associate to the placeholder.",
                        "// ",
                        "// `expression` is a [memory accessor](https://github.com/Jamiras/RATools/wiki/Accessing-Memory), [arithmetic expression](https://github.com/Jamiras/RATools/wiki/Operators#arithmetic-operations), or a function that evaluates to a memory accessor or arithmetic expression.",
                        "// ",
                        "// `format` is one of the following:",
                        "// ",
                        "// * `VALUE` - number (default)",
                        "// * `SECS` - the value is a number of seconds that should be formatted as `MM:SS`",
                        "// * `FRAMES` - the value is a number of frames that should be converted to seconds and displayed as `MM:SS`",
                        "// * `POINTS` - the value should be displayed as a six digit score value followed by the word 'POINTS'",
                        "// * `MILLISECS` - the value is a number of hundredths of a second and will be displayed as `MM:SS.FF`",
                        "// * `MINUTES` - the value is a number of minutes that should be formatted as `HHhMM`",
                        "// * `SECS_AS_MINS` - the value is a number of seconds that should be formatted as `HHhMM`",
                        "// * `FLOAT1` ... `FLOAT6` - the value is formatted to N digits after the decimal (FLOAT1 = 1 digit after the decimal, FLOAT3 = 3 digits after the decimal, etc).",
                        "// * `FIXED1` ... `FIXED3` - the value is formatted with a decimal point N spaces from the end (FIXED1 = 1 digit after the decimal).",
                        "// * `TENS`, `HUNDREDS`, `THOUSANDS` - the value is padded with additional 0s after the end of the value."
                    ]
                },
                new FunctionDefinition() {
                    Key = "rich_presence_lookup",
                    URL = "https://github.com/Jamiras/RATools/wiki/Rich-Presence-Functions#rich_presence_lookupname-expression-dictionary-fallback",
                    Args = [
                        "name",
                        "expression",
                        "dictionary",
                        "fallback"
                    ],
                    CommentDoc = [
                        "// `name` is the name to associate to the placeholder. ",
                        "// ",
                        "// `expression` is a memory accessor, arithmetic expression, or a function that evaluates to a memory accessor or arithmetic expression.",
                        "// ",
                        "// `dictionary` is the [key to value map](https://github.com/Jamiras/RATools/wiki/Variables#dictionaries) used to convert the result of `expression` into a string.",
                        "// ",
                        "// `fallback` is an optional parameter that tells the display string what to display if the value isn't found in the dictionary.",
                        "// If not specified, empty string \"\" will be displayed when a value is not found in the dictionary.",
                        "// ",
                        "// #### Example",
                        "// ",
                        "// ```rascript",
                        "// function lives() => byte(0x05D4) + 1",
                        "// function stage() => byte(0x003A)",
                        "// ",
                        "// stages = new FunctionDefinition() { 1: \"Downtown\", 2: \"Sewers\" }",
                        "// ",
                        "// rich_presence_display(\"new FunctionDefinition() {0}, new FunctionDefinition() {1} lives\",",
                        "//     rich_presence_lookup(\"Stage\", stage(), stages),",
                        "//     rich_presence_value(\"Lives\", lives())",
                        "// )",
                        "// ```"
                    ]
                },
                new FunctionDefinition() {
                    Key = "rich_presence_ascii_string_lookup",
                    URL = "https://github.com/Jamiras/RATools/wiki/Rich-Presence-Functions#rich_presence_ascii_string_lookupname-address-dictionary-fallback",
                    Args = [
                        "name",
                        "address",
                        "dictionary",
                        "fallback"
                    ],
                    CommentDoc = [
                        "// Creates a unique mapping from the keys of `dictionary` to match an ASCII string at `address` and constructs a rich presence lookup.",
                        "// ",
                        "// `name` is the name to associate to the placeholder. ",
                        "// ",
                        "// `address` is the address of the ASCII string.",
                        "// If a memory accessor is passed, it's assumed to be a pointer to the ASCII string.",
                        "// ",
                        "// `dictionary` is the key to value map used to map the ASCII string at `address` to a display string.",
                        "// ",
                        "// `fallback` is an optional parameter that tells the display string what to display if the ASCII string isn't found in the dictionary.",
                        "// If not specified, empty string \"\" will be displayed when a value is not found in the dictionary.",
                        "// ",
                        "// #### Example",
                        "// ",
                        "// ```rascript",
                        "// function lives() => byte(0x05D4) + 1",
                        "// function stage_buffer_address() => 0x003A // 8-byte ASCII string",
                        "// ",
                        "// stages = new FunctionDefinition() { \"LVL_DTWN\": \"Downtown\", \"LVL_SWRS\": \"Sewers\" }",
                        "// ",
                        "// rich_presence_display(\"new FunctionDefinition() {0}, new FunctionDefinition() {1} lives\",",
                        "//     rich_presence_ascii_string_lookup(\"Stage\", stage_buffer_address(), stages),",
                        "//     rich_presence_value(\"Lives\", lives())",
                        "// )",
                        "// ```"
                    ]
                },
                new FunctionDefinition() {
                    Key = "rich_presence_macro",
                    URL = "https://github.com/Jamiras/RATools/wiki/Rich-Presence-Functions#rich_presence_macromacro-expression",
                    Args = [
                        "macro",
                        "expression"
                    ],
                    CommentDoc = [
                        "// `macro` is the name of the built-in macro to use.",
                        "// ",
                        "// * `Number` - number (default)",
                        "// * `Score` - number padded with leading 0s to 6 digits",
                        "// * `Seconds` - the value is a number of seconds that should be formatted as `MM:SS`",
                        "// * `Centiseconds` - the value is a number of hundredths of a second and will be displayed as `MM:SS.FF`",
                        "// * `Minutes` - the value is a number of minutes that should be formatted as `HHhMM`",
                        "// * `ASCIIChar` - the value is converted to a character using the ASCII lookup table",
                        "// * `UnicodeChar` - the value is converted to a character using the UCS2 (16-bit unicode) lookup table",
                        "// * `Float1` ... `Float6` - the value is formatted to N digits after the decimal (Float1 = 1 digit after the decimal, Float3 = 3 digits after the decimal, etc).",
                        "// * `Fixed1` ... `Fixed3` - the value is formatted with a decimal point N spaces from the end (Fixed1 = 1 digit after the decimal).",
                        "// ",
                        "// `expression` is a [memory accessor](https://github.com/Jamiras/RATools/wiki/Accessing-Memory), [arithmetic expression](https://github.com/Jamiras/RATools/wiki/Operators#arithmetic-operations), or a function that evaluates to a memory accessor or arithmetic expression."
                    ]
                },
                new FunctionDefinition() {
                    Key = "rich_presence_conditional_display",
                    URL = "https://github.com/Jamiras/RATools/wiki/Rich-Presence-Functions#rich_presence_conditional_displaycondition-format_string-parameters",
                    Args = [
                        "condition",
                        "format_string",
                        "parameters"
                    ],
                    CommentDoc = [
                        "// Defines a conditional rich presence display string.",
                        "// When executing the rich presence script, each `condition` is examined in order.",
                        "// If a condition is matched, that display string will be used.",
                        "// If no conditions are matched, the default display string will be used.",
                        "// You must still provide a default display string by calling `rich_presence_display`.",
                        "// ",
                        "// This function has the same structure as `rich_presence_display` with the additional `condition` parameter.",
                        "// `condition` must evaluate to one or more [comparisons](https://github.com/Jamiras/RATools/wiki/Operators#comparisons).",
                        "// ",
                        "// #### Example",
                        "// ",
                        "// ```rascript",
                        "// rich_presence_conditional_display(is_title_screen(), \"Title Screen\")",
                        "// rich_presence_display(\"Playing Battle new FunctionDefinition() {0} in new FunctionDefinition() {1}\", ",
                        "//     rich_presence_value(\"Battle\", current_level()),",
                        "//     rich_presence_lookup(\"Landscape\", current_landscape(), landscapes)",
                        "// )",
                        "// ```",
                        "// ",
                        "// **NOTE**: To actually publish the script, you have to copy the script definition to the clipboard (there's a link on the viewer for the rich presence) and paste it into the appropriate field on the website."
                    ]
                },
                new FunctionDefinition() {
                    Key = "leaderboard",
                    URL = "https://github.com/Jamiras/RATools/wiki/Leaderboard-Functions",
                    Args = [
                        "title",
                        "description",
                        "start",
                        "cancel",
                        "submit",
                        "value",
                        "format",
                        "lower_is_better",
                        "id"
                    ],
                    CommentDoc = [
                        "// Defines a leaderboard. `title` and `description` must be strings.",
                        "// ",
                        "// `start`, `cancel`, and `submit` are trigger expressions similar to the [`achievement`](https://github.com/Jamiras/RATools/wiki/Achievement-Functions)'s `trigger` parameter.",
                        "// ",
                        "// `value` is a memory accessor, arithmetic expression, or a function that evaluates to a memory accessor or arithmetic expression.",
                        "// Multiple values may be defined by encasing them in a `max_of(a, b, ...)` function.",
                        "// ",
                        "// `format` is one of the following:",
                        "// ",
                        "// * `VALUE` - number (default)",
                        "// * `SECS` - the value is a number of seconds that should be formatted as `MM:SS`",
                        "// * `FRAMES` - the value is divided by 60 and displayed as `MM:SS`",
                        "// * `POINTS` - the value should be displayed as a zero-padded six digit number",
                        "// * `MILLISECS` - the value is a number of hundredths of a second and will be displayed as `MM:SS.FF`",
                        "// * `MINUTES` - the value is a number of minutes that should be formatted as `HHhMM`",
                        "// * `SECS_AS_MINS` - the value is a number of seconds that should be formatted as `HHhMM`",
                        "// * `FLOAT1` ... `FLOAT6` - the value is formatted to N digits after the decimal (FLOAT1 = 1 digit after the decimal, FLOAT3 = 3 digits after the decimal, etc).",
                        "// * `FIXED1` ... `FIXED3` - the value is formatted with a decimal point N spaces from the end (FIXED1 = 1 digit after the decimal).",
                        "// * `TENS`, `HUNDREDS`, `THOUSANDS` - the value is padded with additional 0s after the end of the value.",
                        "// ",
                        "// if `lower_is_better` is `true`, lower scores will be ranked higher in the leaderboard.",
                        "// ",
                        "// if `id` is provided when calling the `leaderboard` function, the script will generate a local leaderboard definition that the toolkit will merge into the existing leaderboard instead of putting as a separate local leaderboard."
                    ]
                },
                new FunctionDefinition() {
                    Key = "__ornext",
                    URL = "",
                    Args = [
                        "comparison"
                    ],
                    CommentDoc = [
                        "// Undocumented"
                    ]
                }
            };
        }
    }
}