namespace VoidTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnProcessCash = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGetSubData = new System.Windows.Forms.Button();
            this.cboCoupons = new System.Windows.Forms.ComboBox();
            this.btnProcessCoupon = new System.Windows.Forms.Button();
            this.AnytimeScanCheckBox = new System.Windows.Forms.CheckBox();
            this.DirectBalanceInquiryCheckBox = new System.Windows.Forms.CheckBox();
            this.btnProcessCertificate = new System.Windows.Forms.Button();
            this.cboCertificates = new System.Windows.Forms.ComboBox();
            this.btnProcessGiftCard = new System.Windows.Forms.Button();
            this.btnProcessCreditCard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProcessCash
            // 
            this.btnProcessCash.Location = new System.Drawing.Point(13, 238);
            this.btnProcessCash.Name = "btnProcessCash";
            this.btnProcessCash.Size = new System.Drawing.Size(260, 23);
            this.btnProcessCash.TabIndex = 0;
            this.btnProcessCash.Text = "Process Cash";
            this.btnProcessCash.UseVisualStyleBackColor = true;
            this.btnProcessCash.Click += new System.EventHandler(this.btnNewOrder_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(260, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Void/Refund";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnVoid_Click);
            // 
            // btnGetSubData
            // 
            this.btnGetSubData.Location = new System.Drawing.Point(12, 12);
            this.btnGetSubData.Name = "btnGetSubData";
            this.btnGetSubData.Size = new System.Drawing.Size(260, 23);
            this.btnGetSubData.TabIndex = 2;
            this.btnGetSubData.Text = "GetSubwayCardData";
            this.btnGetSubData.UseVisualStyleBackColor = true;
            this.btnGetSubData.Click += new System.EventHandler(this.btnGetCardData_Click);
            // 
            // cboCoupons
            // 
            this.cboCoupons.FormattingEnabled = true;
            this.cboCoupons.Location = new System.Drawing.Point(12, 42);
            this.cboCoupons.Name = "cboCoupons";
            this.cboCoupons.Size = new System.Drawing.Size(260, 21);
            this.cboCoupons.TabIndex = 3;
            // 
            // btnProcessCoupon
            // 
            this.btnProcessCoupon.Location = new System.Drawing.Point(12, 69);
            this.btnProcessCoupon.Name = "btnProcessCoupon";
            this.btnProcessCoupon.Size = new System.Drawing.Size(260, 23);
            this.btnProcessCoupon.TabIndex = 4;
            this.btnProcessCoupon.Text = "Process Coupon";
            this.btnProcessCoupon.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProcessCoupon.UseVisualStyleBackColor = true;
            this.btnProcessCoupon.Click += new System.EventHandler(this.btnProcessCoupon_Click);
            // 
            // AnytimeScanCheckBox
            // 
            this.AnytimeScanCheckBox.AutoSize = true;
            this.AnytimeScanCheckBox.Location = new System.Drawing.Point(13, 180);
            this.AnytimeScanCheckBox.Name = "AnytimeScanCheckBox";
            this.AnytimeScanCheckBox.Size = new System.Drawing.Size(91, 17);
            this.AnytimeScanCheckBox.TabIndex = 5;
            this.AnytimeScanCheckBox.Text = "Anytime Scan";
            this.AnytimeScanCheckBox.UseVisualStyleBackColor = true;
            this.AnytimeScanCheckBox.CheckedChanged += new System.EventHandler(this.AnytimeScanCheckBox_CheckedChanged);
            // 
            // DirectBalanceInquiryCheckBox
            // 
            this.DirectBalanceInquiryCheckBox.AutoSize = true;
            this.DirectBalanceInquiryCheckBox.Location = new System.Drawing.Point(12, 204);
            this.DirectBalanceInquiryCheckBox.Name = "DirectBalanceInquiryCheckBox";
            this.DirectBalanceInquiryCheckBox.Size = new System.Drawing.Size(124, 17);
            this.DirectBalanceInquiryCheckBox.TabIndex = 6;
            this.DirectBalanceInquiryCheckBox.Text = "DirectBalanceInquiry";
            this.DirectBalanceInquiryCheckBox.UseVisualStyleBackColor = true;
            this.DirectBalanceInquiryCheckBox.CheckedChanged += new System.EventHandler(this.DirectBalanceInquiryCheckBox_CheckedChanged);
            // 
            // btnProcessCertificate
            // 
            this.btnProcessCertificate.Location = new System.Drawing.Point(13, 141);
            this.btnProcessCertificate.Name = "btnProcessCertificate";
            this.btnProcessCertificate.Size = new System.Drawing.Size(260, 23);
            this.btnProcessCertificate.TabIndex = 8;
            this.btnProcessCertificate.Text = "Process Certificate";
            this.btnProcessCertificate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProcessCertificate.UseVisualStyleBackColor = true;
            this.btnProcessCertificate.Click += new System.EventHandler(this.btnProcessCertificate_Click);
            // 
            // cboCertificates
            // 
            this.cboCertificates.FormattingEnabled = true;
            this.cboCertificates.Location = new System.Drawing.Point(13, 114);
            this.cboCertificates.Name = "cboCertificates";
            this.cboCertificates.Size = new System.Drawing.Size(260, 21);
            this.cboCertificates.TabIndex = 7;
            // 
            // btnProcessGiftCard
            // 
            this.btnProcessGiftCard.Location = new System.Drawing.Point(13, 267);
            this.btnProcessGiftCard.Name = "btnProcessGiftCard";
            this.btnProcessGiftCard.Size = new System.Drawing.Size(260, 23);
            this.btnProcessGiftCard.TabIndex = 9;
            this.btnProcessGiftCard.Text = "Process GiftCard";
            this.btnProcessGiftCard.UseVisualStyleBackColor = true;
            this.btnProcessGiftCard.Click += new System.EventHandler(this.btnProcessGiftCard_Click);
            // 
            // btnProcessCreditCard
            // 
            this.btnProcessCreditCard.Location = new System.Drawing.Point(12, 296);
            this.btnProcessCreditCard.Name = "btnProcessCreditCard";
            this.btnProcessCreditCard.Size = new System.Drawing.Size(260, 23);
            this.btnProcessCreditCard.TabIndex = 10;
            this.btnProcessCreditCard.Text = "Process CreditCard";
            this.btnProcessCreditCard.UseVisualStyleBackColor = true;
            this.btnProcessCreditCard.Click += new System.EventHandler(this.btnProcessCreditCard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 407);
            this.Controls.Add(this.btnProcessCreditCard);
            this.Controls.Add(this.btnProcessGiftCard);
            this.Controls.Add(this.btnProcessCertificate);
            this.Controls.Add(this.cboCertificates);
            this.Controls.Add(this.DirectBalanceInquiryCheckBox);
            this.Controls.Add(this.AnytimeScanCheckBox);
            this.Controls.Add(this.btnProcessCoupon);
            this.Controls.Add(this.cboCoupons);
            this.Controls.Add(this.btnGetSubData);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnProcessCash);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcessCash;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnGetSubData;
        private System.Windows.Forms.ComboBox cboCoupons;
        private System.Windows.Forms.Button btnProcessCoupon;
        private System.Windows.Forms.CheckBox AnytimeScanCheckBox;
        private System.Windows.Forms.CheckBox DirectBalanceInquiryCheckBox;
        private System.Windows.Forms.Button btnProcessCertificate;
        private System.Windows.Forms.ComboBox cboCertificates;
        private System.Windows.Forms.Button btnProcessGiftCard;
        private System.Windows.Forms.Button btnProcessCreditCard;
    }
}

