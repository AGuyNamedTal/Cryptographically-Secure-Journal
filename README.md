# A Secure Journal :)
A Windows Forms application that connects to a user's Google Drive and stores there an encrypted journal.

This project works as a proof-of-concept of how you can combine security questions for personal data recovery, using Secret Sharing (Shamir Secret Sharing). 

### How it works:

The encrypted journal is just the plain text of the journal encypted with AES-GCM-256 with a key that is directly derived from the password.


The key is derived using Argon2 (which is used extensively throughout to derive all encryption keys).

The user can also add additional security questions, which can be used to change the password.

When using security questions, the key is split using Shamir Secret Sharing into 4 shares (one per question), with a minimum of 3 shares required.
Each share is encrypted with a key that is derived from an answer to a specific security question.


The encrypted shares are then uploaded along with the encrypted journal and each share is correlated with the security question index.

#### Thus you can recover and edit the journal with both the password and answers to 3/4 security questions.
