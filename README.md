# A Secure Journal :)
This is a Windows Forms application that connects to a user's Google Drive and stores there an encrypted journal.
The encrypted journal is the plain text of the journal encypted with AES-GCM-256 with a key that is derived from the password.
The key derivation algrithm is Argon2 and is used extensively.
The key is then split using Shamir Secret Sharing into 4 shares, with a minimum of 3 shares required.
Each share is encrypted with a key that is derived from an answer to a specific security question.
The encrypted shares are then uploaded along with the encrypted journal and each share is correlated with the security question index.
Alternatively, you can use this application without security questions whatsoever.
