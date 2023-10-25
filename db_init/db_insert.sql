-- IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID (N' [cby].[dbo].[accounts]') AND type in (N'U'))
-- IF object_id('cby.dbo.#accounts') is not null
-- BEGIN
    CREATE TABLE [cby].[dbo].[accounts] (
        [BIC]          VARCHAR (50) NOT NULL,
        [account_name] VARCHAR (50) NOT NULL,
        [balance]      INT          CONSTRAINT [DEFAULT_accounts_balance] DEFAULT ((0)) NOT NULL,
        [bank_code]    CHAR (10)    NULL,
        [currency]     CHAR (10)    CONSTRAINT [DEFAULT_accounts_currency] DEFAULT ((886)) NOT NULL,
        CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED ([BIC] ASC)
    );
-- END

    DELETE FROM cby.dbo.accounts;

    INSERT INTO cby.dbo.accounts (BIC, account_name, balance, bank_code, currency)
    VALUES ('CBYEYESA', 'Central Bank of Yemen', 1000000000, 'CBYE', '886'),
           ('KRMBYESA', 'Kuraimi Islamic Bank', 50000000, 'KRMB', '886'),
           ('YKBAYESA', 'Yemen Kuwait Bank', 25500000, 'YKBA', '886'),
           ('CACAYESA', 'CAC Bank', 84000000, 'CACB', '886');
