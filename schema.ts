import { pgTable, text, serial, boolean, timestamp, integer } from "drizzle-orm/pg-core";
import { createInsertSchema } from "drizzle-zod";
import { z } from "zod";

export const invoiceTypes = ["water", "electricity", "gas", "internet", "mobile", "landline"] as const;

export const invoices = pgTable("invoices", {
  id: serial("id").primaryKey(),
  type: text("type", { enum: invoiceTypes }).notNull(),
  amount: integer("amount").notNull(), // Storing as cents/kuruş to avoid floating point issues
  dueDate: timestamp("due_date").notNull(),
  isPaid: boolean("is_paid").default(false).notNull(),
});

// Schema for inserting a user - omit auto-generated fields
export const insertInvoiceSchema = createInsertSchema(invoices, {
  dueDate: z.coerce.date()
}).omit({
  id: true,
  isPaid: true 
});

export type Invoice = typeof invoices.$inferSelect;
export type InsertInvoice = z.infer<typeof insertInvoiceSchema>;
